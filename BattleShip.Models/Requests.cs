namespace BattleShip.Models;

public class InitializeGameRequest(Guid creatorId, GameSettings gameSettings)
{
    public Guid CreatorId { get; set; } = creatorId;
    public GameSettings GameSettings { get; set; } = gameSettings;
}

public class AttackRequest(Guid gameId, Guid playerId, int x, int y)
{
    public Guid GameId { get; set; } = gameId;
    public Guid PlayerId { get; set; } = playerId;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

public class TryJoinGameRequest(Guid sessionId, Guid playerId)
{
    public Guid SessionId { get; set; } = sessionId;
    public Guid PlayerId { get; set; } = playerId;
}