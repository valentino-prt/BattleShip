namespace BattleShip.Models.Requests;

public class AttackRequest(Guid gameId, Guid playerId, int x, int y)
{
    public Guid GameId { get; set; } = gameId;
    public Guid PlayerId { get; set; } = playerId;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}