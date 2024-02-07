using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services
{
    public class GameService : IGameService
    {
        public List<Game> Games { get; } = new List<Game>();

        public GameInfo CreateGame()
        {
            Game newGame = new Game();
            Games.Add(newGame);

            // Initialiser les navires ici ou dans le constructeur de Game selon votre implémentation
            return new GameInfo(newGame.Id, newGame.Boards[0].Ships);
        }

        public GameInfo Attack(Guid gameId, int x, int y)
        {
            Game game = FindGame(gameId) ?? throw new ArgumentException("Invalid game ID");
            AttackResult attackResult;
            char val = game.Boards[game.Player].grid[x, y];
            string? winner = null;

            switch (val)
            {
                case '\0':
                    game.Boards[game.Player].grid[x, y] = 'O';
                    game.Player = game.Player == 0 ? 1 : 0;
                    attackResult = new AttackResult(AttackOutcome.Miss);
                    break;
                case 'O':
                case 'X':
                    attackResult = new AttackResult(AttackOutcome.AlreadyAttacked);
                    break;
                default:
                    int shipNumber = (int)val - '0';
                    var ship = game.Boards[game.Player].Ships[shipNumber];
                    ship.Hits++;
                    game.Boards[game.Player].grid[x, y] = 'X';

                    if (ship.Hits == ship.Length)
                    {
                        attackResult = new AttackResult(AttackOutcome.Sunk, ship.Type.ToString());
                        if (IsGameOver(game))
                        {
                            winner = "Player " + (game.Player.ToString());
                            RemoveGame(gameId);
                        }
                    }
                    else
                    {
                        attackResult = new AttackResult(AttackOutcome.Hit);
                        game.Player = game.Player == 0 ? 1 : 0;
                    }
                    break;


            }


            // Retourner un GameInfo mis à jour
            return new GameInfo(game.Id, game.Boards[game.Player].Ships, winner, attackResult);
        }

        public bool IsGameOver(Game game)
        {
            foreach (var ship in game.Boards[game.Player].Ships)
            {
                if (ship.Hits != ship.Length)
                {
                    return false;
                }
            }
            return true;
        }

        public Game? FindGame(Guid gameId)
        {
            return Games.Find(g => g.Id == gameId);
        }

        public void RemoveGame(Guid gameId)
        {
            Games.RemoveAll(g => g.Id == gameId);
        }
    }
}
