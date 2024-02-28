using BattleShip.Api.Hubs;
using BattleShip.Api.Models;
using BattleShip.Api.Services.Behaviors;
using BattleShip.Api.Utils;
using BattleShip.Models;
using BattleShip.Models.Response;
using Microsoft.AspNetCore.SignalR;
using AttackOutcome = BattleShip.Models.Response.AttackOutcome;

namespace BattleShip.Api.Services;

public class GameService
{
    private readonly ConnectionMapping _connectionMapping;
    private readonly IHubContext<PlayerHub> _hubContext;
    private readonly Dictionary<Guid, GameSession> _sessions = new();

    public GameService(IHubContext<PlayerHub> hubContext, ConnectionMapping connectionMapping)
    {
        _hubContext = hubContext;
        _connectionMapping = connectionMapping;
    }


    private AttackResponse PerformPlayerAttack(Board oppnentBoard, int x, int y)
    {
        var val = oppnentBoard.Grid[x, y];
        switch (val)
        {
            case '\0':
                // Miss
                oppnentBoard.Grid[x, y] = 'O';
                return new AttackResponse(AttackOutcome.Miss, null, false, GameStatus.InProgress);
            case 'O':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress);
            case 'X':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress);
            default:
                // Hit
                oppnentBoard.Grid[x, y] = 'X';
                return new AttackResponse(AttackOutcome.Hit, null, false, GameStatus.InProgress);
        }
    }

    private AttackResponse PerformAittack(Board opponentBoard, Player aiplayer)
    {
        var (x, y) = aiplayer.Behavior.ChooseAttackCoordinates(opponentBoard);
        var val = opponentBoard.Grid[x, y];
        switch (val)
        {
            case '\0':
                // Miss
                opponentBoard.Grid[x, y] = 'O';
                return new AttackResponse(AttackOutcome.Miss, null, false, GameStatus.InProgress);
            case 'O':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress);
            case 'X':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress);
            default:
                // Hit
                opponentBoard.Grid[x, y] = 'X';
                return new AttackResponse(AttackOutcome.Hit, null, false, GameStatus.InProgress);
        }
    }

    public InitializeGameResponse InitializeGame(Guid creatorId, GameSettings gameSettings)
    {
        var player1 = new Player(creatorId);
        var session = new GameSession { Player1 = player1, GameSettings = gameSettings };

        if (gameSettings.Mode == GameMode.SoloVsAI)
        {
            var behavior = gameSettings.Difficulty == AIDifficulty.Easy
                ? (IBehavior)new RandomBehavior()
                : new StrategicBehavior();

            var aiPlayer = new Player(Guid.Empty, behavior);
            session.Player2 = aiPlayer;
            return new InitializeGameResponse(session.Id, player1.Id, player1.Ships, GameStatus.InProgress);
        }

        _sessions.Add(session.Id, session);
        return new InitializeGameResponse(session.Id, player1.Id, player1.Ships, GameStatus.WaitingForOpponent);
    }


    public TryJoinGameResponse TryJoinGame(Guid sessionId, Guid playerId)
    {
        if (_sessions.TryGetValue(sessionId, out var session) && session.Player2 == null)
        {
            var player2 = new Player(playerId);
            session.Player2 = player2;
            return new TryJoinGameResponse(sessionId, player2.Id, player2.Id, GameStatus.InProgress);
        }

        return new TryJoinGameResponse(sessionId, playerId, null, GameStatus.WaitingForOpponent);
    }

    public async Task<AttackResponse> Attack(Guid gameId, Guid player_id, int x, int y)
    {
        var (player, opponent) = GetPlayers(gameId, player_id);

        if (player.isTurn)
        {
            var attackResult = PerformPlayerAttack(opponent.Board, x, y);

            if (attackResult.Result == AttackOutcome.Hit)
            {
                var ship = opponent.Ships.Find(s => s.Name.Equals(attackResult.ShipName));
                if (ship != null)
                    ship.Hits++;
                if (ship.Hits == ship.Length)
                {
                    attackResult.Sunk = true;
                    var isWinner = CheckGameStatus(opponent);
                    if (isWinner) attackResult.GameStatus = GameStatus.Completed;
                }
            }

            // // Si le mode contre l'IA est activé, simuler l'attaque de l'IA ici
            if (opponent.Id == Guid.Empty)
            {
                var aiPlayer = opponent;
                var aiAttackResult = PerformAittack(player.Board, aiPlayer);
                if (aiAttackResult.Result == AttackOutcome.Hit)
                {
                    var ship = player.Ships.Find(s => s.Name.Equals(aiAttackResult.ShipName));
                    if (ship != null)
                        ship.Hits++;
                    if (ship.Hits == ship.Length)
                    {
                        aiAttackResult.Sunk = true;
                        var isWinner = CheckGameStatus(player);
                        if (isWinner) aiAttackResult.GameStatus = GameStatus.Completed;
                    }
                }

                // Envoyer le résultat de l'attaque de l'IA au joueur humain
                var playerConnectionId = _connectionMapping.GetConnectionId(player.Id);
                if (playerConnectionId != null)
                    await _hubContext.Clients.Client(playerConnectionId)
                        .SendAsync("ReceiveAttackResult", aiAttackResult);
            }
            else
            {
                // Envoyer le résultat de l'attaque au joueur adverse
                var opponentConnectionId = _connectionMapping.GetConnectionId(opponent.Id);
                if (opponentConnectionId != null)
                    await _hubContext.Clients.Client(opponentConnectionId)
                        .SendAsync("ReceiveAttackResult", attackResult);
                SwitchPlayer(player, opponent);
            }

            return attackResult;
        }

        return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress);
    }

    private (Player player, Player opponent) GetPlayers(Guid gameId, Guid playerId)
    {
        if (_sessions.TryGetValue(gameId, out var session))
        {
            if (session.Player1.Id == playerId)
                return (session.Player1, session.Player2);
            if (session.Player2.Id == playerId) return (session.Player2, session.Player1);
        }

        throw new ArgumentException("Invalid game ID or player ID");
    }

    private static bool CheckGameStatus(Player player)
    {
        foreach (var ship in player.Ships)
            if (ship.Hits != ship.Length)
                return false;
        return true;
    }

    private static void SwitchPlayer(Player player1, Player player2)
    {
        player1.isTurn = !player1.isTurn;
        player2.isTurn = !player2.isTurn;
    }

    private class GameSession
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public GameSettings GameSettings { get; set; }
    }
}