namespace BattleShip.Models.Response;

public class AttackResponse
{
    public AttackResponse(AttackOutcome result, string? shipName, bool sunk, GameStatus gameStatus,
        Coordinates coordinates)
    {
        Result = result;
        ShipName = shipName;
        Sunk = sunk;
        GameStatus = gameStatus;
        Coordinates = coordinates;
    }

    public AttackOutcome Result { get; set; }
    public string? ShipName { get; set; }
    public bool Sunk { get; set; }
    public GameStatus GameStatus { get; set; }
    public Coordinates Coordinates { get; set; }
}

public enum GameStatus
{
    WaitingForOpponent,
    ReadyToStart,
    InProgress,
    Completed,
    Full,
    DoesNotExist
}

public enum AttackOutcome
{
    Miss,
    Hit,
    Sunk,
    AlreadyAttacked
}