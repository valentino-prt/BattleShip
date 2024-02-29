// using BattleShip.Api.Services;
// using BattleShip.Models;
// using BattleShip.Models.Response;
// using Grpc.Core;
//
// namespace BattleShip.Api.Grpc;
//
// public class BattleShipServiceImpl : BattleShipService.BattleShipServiceBase
// {
//     private readonly GameService _gameService;
//
//     public BattleShipServiceImpl(GameService gameService)
//     {
//         _gameService = gameService;
//     }
//
//     public override Task<InitializeGameResponse> CreateGame(InitializeGameRequest request, ServerCallContext context)
//     {
//         var gameInfo = _gameService.InitializeGame(new Guid(request.CreatorId), request.GameSettings.ToModel());
//         var response = new InitializeGameResponse
//         {
//             SessionId = gameInfo.SessionId.ToString()
//             // Initialisez les autres champs de la réponse ici
//         };
//         return Task.FromResult(response);
//     }
//
//     public override async Task<AttackResponse> PerformAttack(AttackRequest request, ServerCallContext context)
//     {
//         var attackResponse =
//             await _gameService.Attack(new Guid(request.GameId), new Guid(request.PlayerId), request.X, request.Y);
//         return new AttackResponse
//         {
//             // Initialisez les champs de la réponse ici
//         };
//     }
//
//     public override Task<TryJoinGameResponse> JoinGame(TryJoinGameRequest request, ServerCallContext context)
//     {
//         var joinResponse = _gameService.TryJoinGame(new Guid(request.SessionId), new Guid(request.PlayerId));
//         return Task.FromResult(new TryJoinGameResponse
//         {
//             // Initialisez les champs de la réponse ici
//         });
//     }
// }

