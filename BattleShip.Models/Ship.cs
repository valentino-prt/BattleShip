﻿namespace BattleShip.Models;

public class Ship
{
    public Ship(ShipType type, int x, int y, Direction direction)
    {
        // TODO : Change X and Y for Coordinates object
        Type = type;
        X = x; // X coordinate ( top left corner of the grid )
        Y = y; // Y coordinate ( top left corner of the grid )
        Direction = direction;
        Hits = 0;
    }

    public int X { get; set; } // X coordinate
    public int Y { get; set; } // Y coordinate
    public ShipType Type { get; set; } // Type of the ship, must be public to match constructor parameter
    public Direction Direction { get; set; } // Direction of the ship
    public int Hits { get; set; } // Number of hits on the ship

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