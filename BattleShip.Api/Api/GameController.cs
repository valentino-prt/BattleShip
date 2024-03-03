using BattleShip.Api.Services;
using BattleShip.Api.Validators;
using BattleShip.Models.Requests;
using FluentValidation;

namespace BattleShip.Api.Api;

public static class GameController
{
    private static readonly IValidator<InitializeGameRequest> InitializeGameValidator = new InitializeGameValidator();
    private static readonly IValidator<AttackRequest> AttackRequestValidator = new AttackRequestValidator();

    public static void MapGameEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/games",
                async (GameService gameService, InitializeGameRequest request) =>
                {
                    var validationResult = await InitializeGameValidator.ValidateAsync(request);
                    if (!validationResult.IsValid)
                        return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

                    var gameInfo = gameService.InitializeGame(request.CreatorId, request.GameSettings);
                    return Results.Ok(gameInfo);
                })
            .WithName("CreateGame").WithOpenApi();

        endpoints.MapPost("/attack", async (GameService gameService, AttackRequest request) =>
            {
                var validationResult = await AttackRequestValidator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
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