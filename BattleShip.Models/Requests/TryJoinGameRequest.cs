namespace BattleShip.Models.Requests;

public class TryJoinGameRequest(Guid sessionId, Guid playerId)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid PlayerId { get; set; } = playerId;
}