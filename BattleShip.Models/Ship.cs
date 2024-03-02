namespace BattleShip.Models;

public class Ship(ShipType type, int x, int y, Direction direction)
{
    public int X { get; set; } = x; // X coordinate ( top left corner of the grid )
    // X coordinate

    public int Y { get; set; } = y; // Y coordinate ( top left corner of the grid )
    // Y coordinate

    public ShipType Type { get; set; } = type; // Type of the ship, must be public to match constructor parameter
    public Direction Direction { get; set; } = direction; // Direction of the ship
    public int Hits { get; set; } = 0; // Number of hits on the ship

    public string Name => Type switch
    {
        ShipType.PatrolBoat => "Patrol Boat",
        ShipType.Submarine => "Submarine",
        ShipType.Destroyer => "Destroyer",
        ShipType.Battleship => "Battleship",
        ShipType.Carrier => "Carrier",
        _ => "Unknown"
    };

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
    Vertical
}