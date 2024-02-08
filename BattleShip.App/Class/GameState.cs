public class GameState
{
    private static GameState? instance;
    public Guid GameId { get; set; }

    public char[,] UserBoard { get; set; }
    public char[,] AIBoard { get; set; }

    private GameState() 
    {
    }

    public static GameState Instance
    {
        get
        {
            instance ??= new GameState();
            return instance;
        }
    }
}
