namespace BattleShip.Models;

public class Ship(ShipType type, int x, int y, Direction direction)
{
    public int X { get; set; } = x; // X coordinate
    public int Y { get; set; } = y; // Y coordinate
    public ShipType Type { get; set; } = type; // Type of the ship
    public Direction Direction { get; set; } = direction; // Direction of the ship

    public int Hits { get; set; } = 0; // Number of hits on the ship


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