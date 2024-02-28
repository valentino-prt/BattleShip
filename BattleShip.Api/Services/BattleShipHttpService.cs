// using Microsoft.AspNetCore.Mvc;
//
// namespace BattleShip.Api.Services;
//
// public static class BattleShipHttpService
// {
//     public static void MapBattleShipHttpEndpoints(this IEndpointRouteBuilder endpoints)
//     {
//         endpoints.MapPost("/games", (GameService gameService) =>
//             {
//                 var gameInfo = gameService.CreateGame();
//                 return TypedResults.Ok(gameInfo);
//             })
//             .WithName("CreateGame");
//
//         endpoints.MapPut("/games/{gameId}/attack",
//                 (Guid gameId, [FromBody] AttackRequest request, IGameService gameService) =>
//                 {
//                     try
//                     {
//                         var result = gameService.Attack(gameId, request.X, request.Y);
//                         return TypedResults.Ok(result);
//                     }
//                     catch (ArgumentException ex)
//                     {
//                         return Results.NotFound(ex.Message);
//                     }
//                 })
//             .WithName("Attack");
//     }
// }

