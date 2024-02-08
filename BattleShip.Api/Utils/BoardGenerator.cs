﻿using BattleShip.Api.Models;
using BattleShip.Models;

namespace BattleShip.Api.Utils;

public class BoardGenerator
{
    private static readonly Random _random = new();

    public static Board GenerateBoard()
    {
        Board board = new();

        var shipTypes = new List<ShipType>
        {
            ShipType.Carrier,
            ShipType.Battleship,
            ShipType.Destroyer,
            ShipType.Submarine,
            ShipType.PatrolBoat
        };

        foreach (var type in shipTypes)
        {
            var placed = false;
            while (!placed)
            {
                // Generate random position and direction
                var x = _random.Next(board.Width);
                var y = _random.Next(board.Height);
                var direction = _random.Next(2) == 0 ? Direction.Horizontal : Direction.Vertical;

                // Create ship to be placed
                var ship = new Ship(type, x, y, direction);

                // Check if ship can be placed
                if (CanPlaceShip(board, ship))
                {
                    board.AddShip(ship);
                    placed = true;
                }
            }
        }

        return board;
    }

    private static bool CanPlaceShip(Board board, Ship ship)
    {
        // Determine the ship's coverage on the board based on its direction
        var shipEndX = ship.X + (ship.Direction == Direction.Horizontal ? ship.Length : 0);
        var shipEndY = ship.Y + (ship.Direction == Direction.Vertical ? ship.Length : 0);

        // Check if the ship fits within the board bounds
        if (shipEndX > board.Width || shipEndY > board.Height)
            return false;

        // Check for overlap with existing ships
        for (var i = 0; i < ship.Length; i++)
        {
            var currentX = ship.X + (ship.Direction == Direction.Horizontal ? i : 0);
            var currentY = ship.Y + (ship.Direction == Direction.Vertical ? i : 0);

            // Check if the current position is outside the board bounds
            if (currentX >= board.Width || currentY >= board.Height)
                return false;

            // Check if any part of the ship overlaps with existing ships
            foreach (var existingShip in board.Ships)
                for (var j = 0; j < existingShip.Length; j++)
                {
                    var existingShipX = existingShip.X + (existingShip.Direction == Direction.Horizontal ? j : 0);
                    var existingShipY = existingShip.Y + (existingShip.Direction == Direction.Vertical ? j : 0);

                    if (currentX == existingShipX && currentY == existingShipY)
                        return false; // Found an overlap
                }
        }

        return true; // No overlap and within board bounds
    }
}