using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services.Behaviors;

public class StrategicBehavior : IBehavior
{
    private readonly List<Coordinates> _hitTargets = new();
    private readonly Random _random = new();

    public Coordinates ChooseAttackCoordinates(Board opponentBoard)
    {
        if (_hitTargets.Any())
        {
            var lastHit = _hitTargets.Last();
            // Decide whether to adjust the last hit horizontally or vertically
            if (_random.Next(2) == 0)
                // Adjust horizontally
                return new Coordinates(AdjustCoordinate(lastHit.X), lastHit.Y);
            // Adjust vertically
            return new Coordinates(lastHit.X, AdjustCoordinate(lastHit.Y));
        }

        return new RandomBehavior().ChooseAttackCoordinates(opponentBoard);
    }

    private int AdjustCoordinate(int coordinate)
    {
        var adjustment =
            _random.Next(2) == 0 ? -1 : 1;
        return Math.Max(0, Math.Min(coordinate + adjustment, Board.Width - 1));
    }
}