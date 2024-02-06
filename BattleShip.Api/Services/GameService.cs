using System;
namespace BattleShip.Api.Services
{
    public class GameService : IGameService
    {
        public Game CreateGame()
        {
            return new Game();

        }

        public AttackResult Attack(Game game, int x, int y)
        {
            char val = game.Boards[game.Player].grid[x, y];
            switch (val)
            {
                case '\0':
                    game.Boards[game.Player].grid[x, y] = 'O';
                    game.Player = game.Player == 0 ? 1 : 0;
                    return AttackResult.Miss;
                case 'O':
                    return AttackResult.AlreadyAttacked;

                case 'X':
                    return AttackResult.AlreadyAttacked;
                default:
                    int shipNumber = (int)val;
                    game.Boards[game.Player].Ships[shipNumber].Hits++;
                    game.Boards[game.Player].grid[x, y] = 'X';

                    if (game.Boards[game.Player].Ships[shipNumber].Hits == game.Boards[game.Player].Ships[shipNumber].Length)
                    {
                        return AttackResult.Sunk;
                    }
                    else
                    {
                        return AttackResult.Hit;

                    }
            }
        }

        public bool IsGameOver(Game game)
        {
            foreach (var ship in game.Boards[game.Player].Ships)
            {
                if (ship.Hits != ship.Length)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum AttackResult
    {
        Miss,
        Hit,
        Sunk,
        AlreadyAttacked
    }


}
