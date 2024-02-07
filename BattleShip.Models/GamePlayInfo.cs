namespace BattleShip.Models;

public class GamePlayInfo
{
    public Guid GameId { get; set; }
    public string? Winner { get; set; }
    public AttackResult? LastPlayerAttackResult { get; set; }
    public AttackResult? LastAutoPlayerAttackResult { get; set; }

    public GamePlayInfo(Guid gameId, string? winner = null, AttackResult? lastPlayerAttackResult = null, AttackResult? lastAutoPlayerAttackResult = null)
    {
        GameId = gameId;
        Winner = winner;
        LastPlayerAttackResult = lastPlayerAttackResult;
        LastAutoPlayerAttackResult = lastAutoPlayerAttackResult;
    }
}
