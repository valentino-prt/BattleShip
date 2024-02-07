using BattleShip.Api.Models;


namespace BattleShip.Api.Services;

public class GameService : IGameService
{
    public Game CreateGame()
    {
        return new Game();


    }

    public AttackResult Attack(Game game, int x, int y)
    {
        char val = game.Boards[game.Player].grid[x, y];
        switch (val)
        {
            case '\0':
                game.Boards[game.Player].grid[x, y] = 'O';
                game.Player = game.Player == 0 ? 1 : 0;
                return new AttackResult(AttackOutcome.Miss);
            case 'O':
            case 'X':
                return new AttackResult(AttackOutcome.AlreadyAttacked);
            default:
                int shipNumber = (int)val - '0'; // Assurez-vous que cette conversion est correcte selon votre implémentation
                var ship = game.Boards[game.Player].Ships[shipNumber];
                ship.Hits++;
                game.Boards[game.Player].grid[x, y] = 'X';

                if (ship.Hits == ship.Length)
                {
                    return new AttackResult(AttackOutcome.Sunk, ship.Type.ToString()); // Inclure le nom du bateau
                }
                else
                {
                    return new AttackResult(AttackOutcome.Hit);
                }
        }
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
}
