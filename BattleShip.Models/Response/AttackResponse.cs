namespace BattleShip.Models.Response;

public class AttackResponse
{

    public AttackOutcome Result { get; set; }
    public string? ShipName { get; set; }
    public bool Sunk { get; set; }
    public GameStatus GameStatus { get; set; }


    public AttackResponse(AttackOutcome result, string? shipName, bool sunk, GameStatus gameStatus)
    {
        Result = result;
        ShipName = shipName;
        Sunk = sunk;
        GameStatus = gameStatus;
    }
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