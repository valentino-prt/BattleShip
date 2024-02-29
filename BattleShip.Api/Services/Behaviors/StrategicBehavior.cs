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
            // Logique pour choisir autour des cibles touchées précédemment
            var lastHit = _hitTargets.Last();
            // Exemple: choisir une case adjacente à la dernière réussite
            // Cette partie doit être développée en fonction de votre logique spécifique
            return new Coordinates(AdjustCoordinate(lastHit.X), AdjustCoordinate(lastHit.Y));
        }

        return new RandomBehavior().ChooseAttackCoordinates(opponentBoard);
    }

    private int AdjustCoordinate(int coordinate)
    {
        // Logique pour ajuster la coordonnée, par exemple, incrémenter ou décrémenter en restant dans les limites du plateau
        return Math.Max(0, Math.Min(coordinate + _random.Next(-1, 2), Board.Width - 1));
    }
}