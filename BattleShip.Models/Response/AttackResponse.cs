namespace BattleShip.Models.Response;

public class AttackResponse(AttackOutcome result, string? ShipName, bool sunk, GameStatus gameStatus)
{
    public AttackOutcome Result { get; set; } = result;
    public string? ShipName { get; set; } = ShipName;
    public bool Sunk { get; set; } = sunk;
    public GameStatus GameStatus { get; set; } = gameStatus;
}

public enum GameStatus
{
    WaitingForOpponent,
    ReadyToStart,
    InProgress,
    Completed
}

public enum AttackOutcome
{
    Miss,
    Hit,
    Sunk,
    AlreadyAttacked
}