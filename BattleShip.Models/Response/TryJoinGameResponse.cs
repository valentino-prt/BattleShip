namespace BattleShip.Models.Response;

public class TryJoinGameResponse(Guid sessionId, Guid player2Id, List<Ship>? ships, GameStatus status)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid Player2Id { get; set; } = player2Id;
    public List<Ship>? Ships { get; set; } = ships;
    public GameStatus Status { get; set; } = status;
}