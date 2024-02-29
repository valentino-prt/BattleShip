using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services.Behaviors;

public class RandomBehavior : IBehavior
{
    private readonly Random _random = new();

    public Coordinates ChooseAttackCoordinates(Board opponentBoard)
    {
        int x, y;
        do
        {
            x = _random.Next(Board.Width);
            y = _random.Next(Board.Height);
        } while (opponentBoard.Grid[x, y] == 'X' ||
                 opponentBoard.Grid[x, y] == 'O');

        return new Coordinates(x, y);
    }
}