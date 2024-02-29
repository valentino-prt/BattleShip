using System.Text.Json.Serialization;

namespace BattleShip.Models;

public enum GameMode
{
    SoloVsAi,
    Multiplayer
}

public enum AiDifficulty
{
    Easy,
    Medium,
    Hard
}

public class GameSettings
{
    [JsonConstructor]
    public GameSettings(GameMode mode, AiDifficulty? difficulty = null)
    {
        Mode = mode;
        Difficulty = difficulty;
    }

    public GameMode Mode { get; set; }
    public AiDifficulty? Difficulty { get; set; }
}