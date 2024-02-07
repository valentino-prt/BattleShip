namespace BattleShip.Api.Services;
using BattleShip.Models;
using BattleShip.Api.Models;
public interface IGameService
{
    GameInfo CreateGame();
    GameInfo Attack(Guid gameId, int x, int y);
    bool IsGameOver(Game game);
}
