using BattleShip.Api.Models;

namespace BattleShip.Api.Services.Behaviors;

public interface IBehavior
{
    (int x, int y) ChooseAttackCoordinates(Board opponentBoard);
}