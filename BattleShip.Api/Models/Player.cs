using BattleShip.Api.Services.Behaviors;
using BattleShip.Api.Utils;
using BattleShip.Models;

namespace BattleShip.Api.Models;

public class Player
{
    public Player(Guid id) // Constructor for Real Player
    {
        Id = id;
        BoardGenerator generator = new();
        var boardAndShips = generator.GenerateBoard();
        Board = boardAndShips.board;
        Ships = boardAndShips.ships;
        Behavior = null;
    }

    public Player(Guid id, IBehavior behavior) // Constructor for AI Player
    {
        Id = id;
        BoardGenerator generator = new();
        var boardAndShips = generator.GenerateBoard();
        Board = boardAndShips.board;
        Ships = boardAndShips.ships;
        Behavior = behavior;
    }


    public Guid Id { get; set; }

    public Board Board { get; set; } // board of the player
    public List<Ship> Ships { get; set; } // ships of the player

    public bool isTurn { get; set; }

    public IBehavior? Behavior { get; set; }
}