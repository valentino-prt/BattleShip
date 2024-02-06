namespace BattleShip.Api;

public class Game
{
    public Board Board { get; set; } // the board
    public string Status { get; set; } // status of the game
    public string Winner { get; set; } // winner of the game
    public string Player { get; set; } // player whose turn it is
}
