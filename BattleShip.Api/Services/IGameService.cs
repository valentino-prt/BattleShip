namespace BattleShip.Api.Services;
using BattleShip.Api.Models;
public interface IGameService
{
    Game CreateGame();
    AttackResult Attack(Game game, int x, int y);
    bool IsGameOver(Game game);
}
