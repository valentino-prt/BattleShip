namespace BattleShip.Models;

public class GameInfo
{
    public Guid GameId { get; set; }
    public List<Ship> Ships { get; set; }
    public string? Winner { get; set; }
    public AttackResult? LastPlayerAttackResult { get; set; } // Dernier résultat d'attaque du joueur humain
    public AttackResult? LastAutoPlayerAttackResult { get; set; } // Dernier résultat d'attaque de l'AutoPlayer


    public GameInfo(Guid gameId, List<Ship> ships, string? winner = null, AttackResult? lastPlayerAttackResult = null, AttackResult? lastAutoPlayerAttackResult = null)
    {
        GameId = gameId;
        Ships = ships;
        Winner = winner;
        LastPlayerAttackResult = lastPlayerAttackResult;
        LastAutoPlayerAttackResult = lastAutoPlayerAttackResult;
    }
}
