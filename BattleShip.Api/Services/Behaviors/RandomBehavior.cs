using BattleShip.Api.Models;

namespace BattleShip.Api.Services.Behaviors;

public class RandomBehavior : IBehavior
{
    private readonly Random _random = new();

    // TODO : Change (int, int) to a Coordinate class
    public (int x, int y) ChooseAttackCoordinates(Board opponentBoard)
    {
        int x, y;
        do
        {
            x = _random.Next(Board.Width);
            y = _random.Next(Board.Height);
        } while (opponentBoard.Grid[x, y] == 'X' ||
                 opponentBoard.Grid[x, y] == 'O');

        return (x, y);
    }
}