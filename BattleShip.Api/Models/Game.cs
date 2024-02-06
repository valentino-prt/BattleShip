namespace BattleShip.Api;

public class Game
{
    public List<Board> Boards { get; set; } = [new Board(), new Board()]; // boards for each player (0 for player 1, 1 for player Ai)
    public Guid Id { get; } = new Guid();
    public int Player { get; set; } = 0; // current player (0 or 1)
}
