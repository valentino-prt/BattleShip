namespace BattleShip.Models;

public class GameInitInfo
{
    public Guid GameId { get; set; }
    public List<Ship> Ships { get; set; }

    public GameInitInfo(Guid gameId, List<Ship> ships)
    {
        GameId = gameId;
        Ships = ships;
    }
}
