using BattleShip.Api.Services;
using BattleShip.Models;

namespace BattleShip.Api.Api;

public static class GameController
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/games", (GameService gameService, InitializeGameRequest request) =>
            {
                // Vous pouvez ajouter une validation ici si nécessaire
                var gameInfo = gameService.InitializeGame(request.CreatorId, request.GameSettings);
                return Task.FromResult(Results.Ok(gameInfo));
            })
            .WithName("CreateGame");

        endpoints.MapPost("/attack", (GameService gameService, AttackRequest request) =>
            {
                var attackResponse = gameService.Attack(request.GameId, request.PlayerId, request.X, request.Y);
                return Task.FromResult(Results.Ok(attackResponse));
            })
            .WithName("PerformAttack");

        endpoints.MapPost("/join", (GameService gameService, TryJoinGameRequest request) =>
            {
                var joinResponse = gameService.TryJoinGame(request.SessionId, request.PlayerId);
                return Task.FromResult(Results.Ok(joinResponse));
            })
            .WithName("JoinGame");
    }
}