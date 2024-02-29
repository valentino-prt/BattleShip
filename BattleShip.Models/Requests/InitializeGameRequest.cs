namespace BattleShip.Models.Requests;

public class InitializeGameRequest(Guid creatorId, GameSettings gameSettings)
{
    public Guid CreatorId { get; set; } = creatorId;
    public GameSettings GameSettings { get; set; } = gameSettings;
}