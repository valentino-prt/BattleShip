namespace BattleShip.Models.Response;

public class InitializeGameResponse(Guid sessionId, Guid playerId, List<Ship> ships, GameStatus status)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid PlayerId { get; set; } = playerId;
    public List<Ship> Ships { get; set; } = ships;
    public GameStatus Status { get; set; } = status;
}