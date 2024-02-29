using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services.Behaviors;

public interface IBehavior
{
    Coordinates ChooseAttackCoordinates(Board opponentBoard);
}