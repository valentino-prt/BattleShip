using BattleShip.Api.Models;

namespace BattleShip.Api.Services.Behaviors;

public interface IBehavior
{
    // TODO : Change (int, int) to a Coordinate class
    (int x, int y) ChooseAttackCoordinates(Board opponentBoard);
}