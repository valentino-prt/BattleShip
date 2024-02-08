using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Services;

public interface IGameService
{
    GameInitInfo CreateGame();
    GamePlayInfo Attack(Guid gameId, int x, int y);
    bool IsGameOver(Game game);
}