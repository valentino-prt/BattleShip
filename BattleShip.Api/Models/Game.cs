namespace BattleShip.Api;

public class Game
{
    public Board BoardUser { get; set; } // board for user
    public Board BoardIa {get; set;} // board for IA
    public Guid Id {get;} = new Guid();
    public string Status { get; set; } // status of the game
    public string Winner { get; set; } // winner of the game
    public string Player { get; set; } // player whose turn it is
}
