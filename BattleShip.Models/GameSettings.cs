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
    // Constructeur pour le mode SoloVsAI
    public GameSettings(GameMode mode, AIDifficulty difficulty)
    {
        if (mode == GameMode.SoloVsAI)
        {
            Mode = mode;
            Difficulty = difficulty;
        }
        else
        {
            throw new ArgumentException("Difficulty should not be set for multiplayer mode.");
        }
    }

    // Constructeur pour le mode Multiplayer
    public GameSettings(GameMode mode)
    {
        if (mode == GameMode.Multiplayer)
        {
            Mode = mode;
            Difficulty = null;
        }
        else
        {
            throw new ArgumentException("This constructor should only be used for multiplayer mode.");
        }
    }

    public GameMode Mode { get; set; }
    public AIDifficulty? Difficulty { get; set; }
}