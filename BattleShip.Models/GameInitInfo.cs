namespace BattleShip.Models;

public class GameInitInfo
{
    public Guid GameId { get; set; }
    public List<Ship> Ships { get; set; }
}
