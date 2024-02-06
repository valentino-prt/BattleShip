﻿namespace BattleShip.Api.Services
{
    public interface IGameService
    {
        Game CreateGame();
        AttackResult Attack(Game game, int x, int y);
        bool IsGameOver(Game game);
    }
}
