using BattleShip.Api.Utils;

namespace BattleShip.Api.Models;

public class Game
{
    public Game()
    {
        Boards = new List<Board>
        {
            BoardGenerator.GenerateBoard(),
            BoardGenerator.GenerateBoard()
        };
    }

    public List<Board> Boards { get; set; } // boards for each player (0 for player 1, 1 for player Ai)
    public Guid Id { get; } = Guid.NewGuid();
    public int Player { get; set; } = 0; // current player (0 or 1)
}