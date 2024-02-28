using BattleShip.Api.Utils;
using BattleShip.Models;

namespace BattleShip.Api.Models;

public class Player
{
    public Player(Guid id)
    {
        Id = id;
        BoardGenerator generator = new();
        var boardAndShips = generator.GenerateBoard();
        Board = boardAndShips.board;
        Ships = boardAndShips.ships;
    }

    public Guid Id { get; set; }
    public Board Board { get; set; } // board of the player
    public List<Ship> Ships { get; set; } // ships of the player

    public bool isTurn { get; set; }

    // public Behavior behavior { get; set; }
}