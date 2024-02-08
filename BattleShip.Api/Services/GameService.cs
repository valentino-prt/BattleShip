using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services;

public class GameService : IGameService
{
    private readonly AutoPlayer _autoPlayer = new();
    public List<Game> Games { get; } = new();


    public GameInitInfo CreateGame()
    {
        Game newGame = new();
        Games.Add(newGame);

            // Initialiser les navires ici ou dans le constructeur de Game selon votre implémentation
            return new GameInitInfo{GameId = newGame.Id, Ships = newGame.Boards[0].Ships};
        }


    public GamePlayInfo Attack(Guid gameId, int x, int y)
    {
        var game = FindGame(gameId) ?? throw new ArgumentException("Invalid game ID");
        // Effectuer l'attaque du joueur
        var playerAttackResult = PerformAttack(game, x, y, game.Player);
        string? winner = null;

        if (!IsGameOver(game))
        {
            // Passer le tour à l'AutoPlayer
            game.Player = (game.Player + 1) % 2;
            var (autoX, autoY) = _autoPlayer.ChooseAttackPosition(game);
            // Effectuer l'attaque de l'AutoPlayer
            var autoPlayerAttackResult = PerformAttack(game, autoX, autoY, game.Player);

            if (!IsGameOver(game))
            {
                game.Player = (game.Player + 1) % 2;
            }
            else
            {
                winner = DetermineWinner(game);
                RemoveGame(game.Id);
            }

            // Mettre à jour le GameInfo avec les résultats de l'attaque de l'AutoPlayer
            return new GamePlayInfo(game.Id, winner, playerAttackResult, autoPlayerAttackResult);
        }

        winner = DetermineWinner(game);
        RemoveGame(game.Id);
        return new GamePlayInfo(game.Id, winner, playerAttackResult);
    }

    public bool IsGameOver(Game game)
    {
        foreach (var ship in game.Boards[game.Player].Ships)
            if (ship.Hits != ship.Length)
                return false;
        return true;
    }

    private AttackResult PerformAttack(Game game, int x, int y, int player)
    {
        var val = game.Boards[player].Grid[x, y];
        switch (val)
        {
            case '\0':
                game.Boards[player].Grid[x, y] = 'O';
                return new AttackResult(AttackOutcome.Miss, [x, y]);
            case 'O':
            case 'X':
                return new AttackResult(AttackOutcome.AlreadyAttacked, [x, y]);
            default:
                var shipNumber = val - '0';
                var ship = game.Boards[player].Ships[shipNumber];
                ship.Hits++;
                game.Boards[player].Grid[x, y] = 'X';
                if (ship.Hits == ship.Length)
                    return new AttackResult(AttackOutcome.Sunk, [x, y], ship.Type.ToString());
                return new AttackResult(AttackOutcome.Hit, [x, y], ship.Type.ToString());
        }
    }

    public Game? FindGame(Guid gameId)
    {
        return Games.Find(g => g.Id == gameId);
    }

    public void RemoveGame(Guid gameId)
    {
        Games.RemoveAll(g => g.Id == gameId);
    }

    private string DetermineWinner(Game game)
    {
        return game.Player == 0 ? "AutoPlayer" : "Player";
    }
}