namespace BattleShip.Models;

public class AttackResult(AttackOutcome outcome, List<int> coordinates, string? shipName = null)
{
    public AttackOutcome Outcome { get; set; } = outcome;
    public string? ShipName { get; set; } = shipName;

    public List<int> Coordinates { get; set; } = coordinates;
}

public enum AttackOutcome
{
    Miss,
    Hit,
    Sunk,
    AlreadyAttacked
}
