using BattleShip.Models;

namespace BattleShip.Api.Models;

class GameSession
{
    public Guid Id { get; } = Guid.NewGuid();
    public Player Player1 { get; set; }
    public Player Player2 { get; set; }
    public GameSettings GameSettings { get; set; }
}