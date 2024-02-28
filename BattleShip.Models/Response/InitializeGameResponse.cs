namespace BattleShip.Models.Response;

public class InitializeGameResponse(Guid sessionId, Guid player1Id, List<Ship> ships, GameStatus status)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid Player1Id { get; set; } = player1Id;
    public List<Ship> Ships { get; set; } = ships;
    public GameStatus Status { get; set; } = status;
}