namespace BattleShip.Api.Models;

public class AttackResult
{
    public AttackOutcome Outcome { get; set; }
    public string ShipName { get; set; }

    public AttackResult(AttackOutcome outcome, string shipName = null)
    {
        Outcome = outcome;
        ShipName = shipName;
    }
}

public enum AttackOutcome
{
    Miss,
    Hit,
    Sunk,
    AlreadyAttacked
}
