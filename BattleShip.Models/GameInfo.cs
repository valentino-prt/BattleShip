namespace BattleShip.Models;

public class GameInfo
{
    public Guid GameId { get; set; }
    public List<Ship> Ships { get; set; }

    public String? Winner { get; set; }

    public AttackResult? LastAttackResult { get; set; }

    public GameInfo(Guid gameId, List<Ship> ships, String? winner = null, AttackResult? lastAttackResult = null)
    {
        GameId = gameId;
        Ships = ships;
        Winner = winner;
        LastAttackResult = lastAttackResult;
    }
}