public class GameState
{
    private static GameState? instance;
    public Guid GameId { get; private set; }
    public char[,] UserBoard { get; private set; }
    public char[,] AIBoard { get; private set; }

    // Constructeur privé pour empêcher l'instanciation en dehors de la classe
    private GameState(Guid gameId, char[,] userBoard, char[,] aiBoard)
    {
        GameId = gameId;
        UserBoard = userBoard;
        AIBoard = aiBoard;
    }

    // Méthode pour initialiser l'instance singleton
    public static void InitializeInstance(Guid gameId, char[,] userBoard, char[,] aiBoard)
    {
        if (instance == null)
        {
            instance = new GameState(gameId, userBoard, aiBoard);
        }
        else
        {
            instance.UpdateState(gameId, userBoard, aiBoard);
        }
    }

    // Propriété pour accéder à l'instance singleton
    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                throw new InvalidOperationException("GameState must be initialized first.");
            }
            return instance;
        }
    }

    // Méthode pour mettre à jour l'état du jeu
    public void UpdateState(Guid gameId, char[,] userBoard, char[,] aiBoard)
    {
        GameId = gameId;
        UserBoard = userBoard;
        AIBoard = aiBoard;
    }
}