using BattleShip.Models;

namespace BattleShip.Api.Api;

public class InitializeGameRequest
{
    public Guid CreatorId { get; set; }
    public GameSettings GameSettings { get; set; }
}

public class AttackRequest
{
    public Guid GameId { get; set; }
    public Guid PlayerId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public class TryJoinGameRequest
{
    public Guid SessionId { get; set; }
    public Guid PlayerId { get; set; }
}