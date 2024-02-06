using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Primitives;

namespace BattleShip.Api;

public class Board
{

    public readonly int width = 10;
    public readonly int height = 10;

    public char[,] grid { get; set; } // grid of the board

    private List<Ship> _ships = new List<Ship>(); // list of ships on the board, now private

    public int attacks { get; set; } // number of attacks made on the board



    public Board()
    {
        this.grid = InitializeBoard();
        this.attacks = 0;

    }

    public void PrintBoard()
    {

        Console.Write("  ");
        for (var col = 0; col < width; col++)
        {
            Console.Write($"\t{col}");
        }
        Console.WriteLine();

        for (var i = 0; i < width; i++)
        {
            Console.Write($"{i} |");
            for (var j = 0; j < height; j++)
            {
                Console.Write($"\t{(grid[i, j] == '\0' ? '.' : grid[i, j])}");
            }
            Console.WriteLine();
        }
    }


    private char[,] InitializeBoard()
    {
        var board = new char[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                board[i, j] = '\0';
            }
        }
        return board;
    }

    public void AddShip(Ship ship)
    {
        // Add the ship to the list
        _ships.Add(ship);

        // Update the grid based on the ship's position and length
        UpdateGridWithShip(ship);
    }

    private void UpdateGridWithShip(Ship ship)
    {
        for (int i = 0; i < ship.Length; i++)
        {
            if (ship.Direction == Direction.Horizontal)
            {
                grid[ship.X + i, ship.Y] = ship.Type.ToString()[0];
            }
            else // Direction.Vertical
            {
                grid[ship.X, ship.Y + i] = ship.Type.ToString()[0];
            }
        }
    }

    public IEnumerable<Ship> Ships => _ships.AsReadOnly(); // Expose ships as a read-only collection


}


