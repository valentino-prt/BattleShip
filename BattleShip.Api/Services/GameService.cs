﻿using BattleShip.Api.Hubs;
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

    private AttackResponse PerformPlayerAttack(Player opponent, int x, int y)
    {
        var opponentBoard = opponent.Board;
        var val = opponentBoard.Grid[x, y];

        switch (val)
        {
            case '\0':
                // Miss
                opponentBoard.Grid[x, y] = 'O';
                return new AttackResponse(AttackOutcome.Miss, null, false, GameStatus.InProgress,
                    new Coordinates(x, y));
            case 'O':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress,
                    new Coordinates(x, y));
            case 'X':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress,
                    new Coordinates(x, y));
            default:
                // Hit
                opponentBoard.Grid[x, y] = 'X';
                var shipname = opponent.Ships.Find(s => s.Name[0] == val)?.Name;

                return new AttackResponse(AttackOutcome.Hit, shipname, false, GameStatus.InProgress,
                    new Coordinates(x, y));
        }
    }

    private AttackResponse PerformAittack(Player opponent, Player aiPlayer)
    {
        var opponentBoard = opponent.Board;
        var coordinates = aiPlayer.Behavior.ChooseAttackCoordinates(opponentBoard);
        var val = opponentBoard.Grid[coordinates.X, coordinates.Y];
        switch (val)
        {
            case '\0':
                // Miss
                opponentBoard.Grid[coordinates.X, coordinates.Y] = 'O';
                return new AttackResponse(AttackOutcome.Miss, null, false, GameStatus.InProgress, coordinates);
            case 'O':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress,
                    coordinates);
            case 'X':
                // Already attacked
                return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress,
                    coordinates);
            default:
                // Hit
                opponentBoard.Grid[coordinates.X, coordinates.Y] = 'X';
                var shipname = opponent.Ships.Find(s => s.Name[0] == val)?.Name;
                return new AttackResponse(AttackOutcome.Hit, shipname, false, GameStatus.InProgress, coordinates);
        }
    }

    public InitializeGameResponse InitializeGame(Guid creatorId, GameSettings gameSettings)
    {
        var player1 = new Player(creatorId);
        player1.IsTurn = true;
        var session = new GameSession { Player1 = player1, GameSettings = gameSettings };

        if (gameSettings.Mode == GameMode.SoloVsAi)
        {
            var behavior = gameSettings.Difficulty == AiDifficulty.Easy
                ? (IBehavior)new RandomBehavior()
                : new StrategicBehavior();

            var aiPlayer = new Player(Guid.Empty, behavior);
            session.Player2 = aiPlayer;

            _sessions.Add(session.Id, session);
            return new InitializeGameResponse(session.Id, player1.Id, player1.Ships, GameStatus.InProgress);
        }

        _sessions.Add(session.Id, session);
        return new InitializeGameResponse(session.Id, player1.Id, player1.Ships, GameStatus.WaitingForOpponent);
    }


    public TryJoinGameResponse TryJoinGame(Guid sessionId, Guid playerId)
    {
        if (_sessions.TryGetValue(sessionId, out var session))
        {
            if (session.Player2 == null)
            {
                var player2 = new Player(playerId);
                session.Player2 = player2;
                // Successfully joined the game
                return new TryJoinGameResponse(sessionId, player2.Id, player2.Ships, GameStatus.InProgress);
            }

            // Session already full
            return new TryJoinGameResponse(sessionId, playerId, null, GameStatus.Full);
        }

        // If session does not exist or other error
        return new TryJoinGameResponse(sessionId, playerId, null, GameStatus.DoesNotExist);
    }


    public async Task<AttackResponse> Attack(Guid gameId, Guid player_id, int x, int y)
    {
        var (player, opponent) = GetPlayers(gameId, player_id);

        if (player.IsTurn)
        {
            var attackResult = PerformPlayerAttack(opponent, x, y);
            if( attackResult.Result == AttackOutcome.AlreadyAttacked)  return attackResult;
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

            // Si le mode contre l'IA est activé, simuler l'attaque de l'IA ici
            if (opponent.Id == Guid.Empty)
            {
                var aiPlayer = opponent;
                var aiAttackResult = PerformAittack(player, aiPlayer);
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

        return new AttackResponse(AttackOutcome.AlreadyAttacked, null, false, GameStatus.InProgress,
            new Coordinates(x, y));
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
        player1.IsTurn = !player1.IsTurn;
        player2.IsTurn = !player2.IsTurn;
    }
}