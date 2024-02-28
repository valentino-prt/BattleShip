// using BattleShip.Api.Models;
//
// namespace BattleShip.Api.Services;
//
// public class AutoPlayerService
// {
//     public enum Strategy
//     {
//         Random,
//         Strategic
//     }
//
//
//     private readonly HashSet<(int, int)> _previousAttacks = new();
//     private readonly Random _random = new();
//     private readonly Strategy _strategy;
//
//     public AutoPlayerService(Strategy strategy)
//     {
//         _strategy = strategy;
//     }
//
//
//     public (int X, int Y) GetRandomAttackPosition(Game game)
//     {
//         // Assumer un plateau de jeu de taille 10x10 pour cet exemple
//         var boardSize = 10;
//         int x, y;
//
//         // Choisissez une position aléatoire qui n'a pas encore été attaquée
//         do
//         {
//             x = _random.Next(boardSize);
//             y = _random.Next(boardSize);
//         } while (_previousAttacks.Contains((x, y)));
//
//         // Ajouter la position choisie à l'ensemble des attaques précédentes pour éviter les répétitions
//         _previousAttacks.Add((x, y));
//
//         return (x, y);
//     }
//
//     public (int X, int Y) ChooseAttackPosition(Game game)
//     {
//         return _strategy switch
//         {
//             Strategy.Random => GetRandomAttackPosition(game),
//             Strategy.Strategic => GetStrategicAttackPosition(game),
//             _ => GetRandomAttackPosition(game)
//         };
//     }
//
//     public (int X, int Y) GetStrategicAttackPosition(Game game)
//     {
//         // For the first attack, choose a random position in the middle of the board
//         // The four by four squares in the middle of the board are likely to contain a carrier ship or battleship
//
//         return GetRandomAttackPosition(game);
//     }
//
//
//     public void Reset()
//     {
//         // Réinitialiser l'historique des attaques pour une nouvelle partie
//         _previousAttacks.Clear();
//     }
// }

