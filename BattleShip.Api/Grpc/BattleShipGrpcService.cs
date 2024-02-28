// using BattleShip.Api.Grpc;
// using Grpc.Core;
// using AttackOutcome = BattleShip.Models.AttackOutcome;
// using Direction = BattleShip.Models.Direction;
// using GameInitInfo = BattleShip.Api.Grpc.GameInitInfo;
// using Ship = BattleShip.Api.Grpc.Ship;
//
// namespace BattleShip.Api.Services;
//
// public class BattleShipGrpcService : BattleShipService.BattleShipServiceBase
// {
//     private readonly IGameService _gameService;
//
//     public BattleShipGrpcService(IGameService gameService)
//     {
//         _gameService = gameService;
//     }
//
//     public override Task<GameInitInfo> CreateGame(Empty request, ServerCallContext context)
//     {
//         var gameInfo = _gameService.CreateGame();
//         var result = new GameInitInfo
//         {
//             GameId = gameInfo.GameId.ToString()
//         };
//         foreach (var ship in gameInfo.Ships)
//         {
//             var shipInfo = new Ship
//             {
//                 Length = ship.Length,
//                 Type = ship.Type.ToString(),
//                 X = ship.X,
//                 Y = ship.Y,
//                 Direction = ship.Direction switch
//                 {
//                     Direction.Horizontal => Grpc.Direction.Horizontal,
//                     Direction.Vertical => Grpc.Direction.Vertical,
//                     _ => Grpc.Direction.Horizontal
//                 }
//             };
//             result.Ships.Add(shipInfo);
//         }
//
//         return Task.FromResult(result);
//     }
//
//     public override Task<GamePlayInfo> Attack(Grpc.AttackRequest request, ServerCallContext context)
//     {
//         var guid = Guid.Parse(request.GameId);
//         var result = _gameService.Attack(guid, request.X, request.Y);
//         var gamePlayInfo = new GamePlayInfo
//         {
//             GameId = guid.ToString(),
//             Winner = result.Winner,
//             LastPlayerAttackResult = new AttackResult
//             {
//                 Coordinates = new Position
//                 {
//                     X = result.LastPlayerAttackResult.Coordinates[0],
//                     Y = result.LastPlayerAttackResult.Coordinates[1]
//                 },
//                 Outcome = result.LastPlayerAttackResult.Outcome switch
//                 {
//                     AttackOutcome.Miss => Grpc.AttackOutcome.Miss,
//                     AttackOutcome.Hit => Grpc.AttackOutcome.Hit,
//                     AttackOutcome.Sunk => Grpc.AttackOutcome.Sunk,
//                     _ => Grpc.AttackOutcome.Miss
//                 }
//             }
//         };
//         return Task.FromResult(gamePlayInfo);
//     }
// }

