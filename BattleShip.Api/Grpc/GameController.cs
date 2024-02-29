using BattleShip.Api.Services;
using BattleShip.Models.Requests;
using BattleShip.Models.Responses;
using Grpc.Core;

namespace BattleShip.Api.Grpc;

public class BattleShipServiceImpl : BattleShipService.BattleShipServiceBase
{
    private readonly GameService _gameService;

    public BattleShipServiceImpl(GameService gameService)
    {
        _gameService = gameService;
    }

    public Task<InitializeGameResponse> CreateGame(InitializeGameRequest request, ServerCallContext context)
    {
        var gameInfo = _gameService.InitializeGame(request.CreatorId, request.GameSettings);
        return Task.FromResult(gameInfo);
    }

    // public override async Task<AttackResponse> PerformAttack(AttackRequest request, ServerCallContext context)
    // {
    //     var attackResponse = await gameService.Attack(request.GameId, request.playerID, request.X, request.Y);
    //     return Results.Ok(attackResponse);
    // }
    //
    // public override Task<TryJoinGameResponse> JoinGame(TryJoinGameRequest request, ServerCallContext context)
    // {
    //     var joinResponse = gameService.TryJoinGame(request.SessionId, request.PlayerId);
    //     return Results.Ok(joinResponse);
    // }
}