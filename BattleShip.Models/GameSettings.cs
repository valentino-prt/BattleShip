using System.Text.Json.Serialization;

namespace BattleShip.Models;

public enum GameMode
{
    SoloVsAI,
    Multiplayer
}

public enum AIDifficulty
{
    Easy,
    Medium,
    Hard
}

public class GameSettings
{
    [JsonConstructor]
    public GameSettings(GameMode mode, AIDifficulty? difficulty = null)
    {
        Mode = mode;
        Difficulty = difficulty;
    }

    public GameMode Mode { get; set; }
    public AIDifficulty? Difficulty { get; set; }
}