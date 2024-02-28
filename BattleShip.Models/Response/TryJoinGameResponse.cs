namespace BattleShip.Models.Response;

public class TryJoinGameResponse(Guid sessionId, Guid player1Id, Guid? player2Id, GameStatus status)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid Player1Id { get; set; } = player1Id;
    public Guid? Player2Id { get; set; } = player2Id;
    public GameStatus Status { get; set; } = status;
}