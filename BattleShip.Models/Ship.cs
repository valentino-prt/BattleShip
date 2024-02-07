namespace BattleShip.Models;

public class Ship
{
    public int X { get; set; } // X coordinate
    public int Y { get; set; } // Y coordinate
    public ShipType Type { get; set; } // Type of the ship
    public Direction Direction { get; set; } // Direction of the ship

    public int Hits { get; set; } // Number of hits on the ship



    public Ship(ShipType type, int x, int y, Direction direction)
    {
        Type = type;
        X = x;
        Y = y;
        Direction = direction;
        Hits = 0;

    }

    public int Length => Type switch
    {
        ShipType.PatrolBoat => 2,
        ShipType.Submarine => 3,
        ShipType.Destroyer => 3,
        ShipType.Battleship => 4,
        ShipType.Carrier => 5,
        _ => 0
    };
}

public enum ShipType
{
    Carrier,
    Battleship,
    Destroyer,
    Submarine,
    PatrolBoat
}

public enum Direction
{
    Horizontal,
    Vertical,
}
