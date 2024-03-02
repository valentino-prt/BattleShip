using BattleShip.Api.Services;
using BattleShip.Models.Requests;

namespace BattleShip.Api.Api;

public static class GameController
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/games", (GameService gameService, InitializeGameRequest request) =>
            {
                // Vous pouvez ajouter une validation ici si nécessaire
                var gameInfo = gameService.InitializeGame(request.CreatorId, request.GameSettings);
                return Results.Ok(gameInfo);
            })
            .WithName("CreateGame").WithOpenApi();

        endpoints.MapPost("/attack", async (GameService gameService, AttackRequest request) =>
            {
                var attackResponse = await gameService.Attack(request.GameId, request.PlayerId, request.X, request.Y);
                return Results.Ok(attackResponse);
            })
            .WithName("PerformAttack").WithOpenApi();

        endpoints.MapPost("/join", async (GameService gameService, TryJoinGameRequest request) =>
            {
                var joinResponse = await gameService.TryJoinGame(request.SessionId, request.PlayerId);
                return Results.Ok(joinResponse);
            })
            .WithName("JoinGame").WithOpenApi();
    }
}