using BattleShip.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip.Api.Services
{
    public class AutoPlayer
    {
        private readonly Random _random = new();
        private readonly HashSet<(int, int)> _previousAttacks = new();

        public (int X, int Y) ChooseAttackPosition(Game game)
        {
            // Assumer un plateau de jeu de taille 10x10 pour cet exemple
            int boardSize = 10;
            int x, y;

            // Choisissez une position aléatoire qui n'a pas encore été attaquée
            do
            {
                x = _random.Next(boardSize);
                y = _random.Next(boardSize);
            } while (_previousAttacks.Contains((x, y)));

            // Ajouter la position choisie à l'ensemble des attaques précédentes pour éviter les répétitions
            _previousAttacks.Add((x, y));

            return (x, y);
        }

        public void Reset()
        {
            // Réinitialiser l'historique des attaques pour une nouvelle partie
            _previousAttacks.Clear();
        }
    }
}
