public class GameState
{
    private static GameState? instance;
    public Guid GameId { get; private set; }
    public Guid PlayerId { get; private set; }
    public char[,] UserBoard { get; private set; }
    public char[,] OpponentBoard { get; private set; }

    // Constructeur privé pour empêcher l'instanciation en dehors de la classe
    private GameState(Guid gameId, char[,] userBoard, char[,] opponentBoard, Guid playerId)
    {
        GameId = gameId;
        PlayerId = playerId;
        UserBoard = userBoard;
        OpponentBoard = opponentBoard;
    }

    // Méthode pour initialiser l'instance singleton
    public static void InitializeInstance(Guid gameId, char[,] userBoard, char[,] opponentBoard, Guid playerId)
    {
        if (instance == null)
        {
            instance = new GameState(gameId, userBoard, opponentBoard, playerId);
        }
        else
        {
            instance.UpdateState(gameId, userBoard, opponentBoard, playerId);
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
    public void UpdateState(Guid gameId, char[,] userBoard, char[,] opponentBoard, Guid playerId)
    {
        GameId = gameId;
        PlayerId = playerId;
        UserBoard = userBoard;
        OpponentBoard = opponentBoard;
    }
}