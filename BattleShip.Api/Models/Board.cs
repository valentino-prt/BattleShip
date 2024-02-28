using BattleShip.Models;

namespace BattleShip.Api.Models;

public class Board
{
    public readonly int Height = 10;

    public readonly int Width = 10;

    public Board()
    {
        Grid = InitializeBoard();
        attacks = 0;
    }

    public char[,] Grid { get; set; } // grid of the board

    public int attacks { get; set; } // number of attacks made on the board

    public void PrintBoard()
    {
        Console.Write("  ");
        for (var col = 0; col < Width; col++) Console.Write($"\t{col}");
        Console.WriteLine();

        for (var i = 0; i < Width; i++)
        {
            Console.Write($"{i} |");
            for (var j = 0; j < Height; j++) Console.Write($"\t{(Grid[i, j] == '\0' ? '.' : Grid[i, j])}");
            Console.WriteLine();
        }
    }


    private char[,] InitializeBoard()
    {
        var board = new char[Width, Height];
        for (var i = 0; i < Width; i++)
        for (var j = 0; j < Height; j++)
            board[i, j] = '\0';
        return board;
    }

    public void UpdateGridWithShip(Ship ship)
    {
        var shipChar = ship.Name[0];

        for (var i = 0; i < ship.Length; i++)
            if (ship.Direction == Direction.Horizontal)
                Grid[ship.X + i, ship.Y] = shipChar;
            else // Direction.Vertical
                Grid[ship.X, ship.Y + i] = shipChar;
    }
}