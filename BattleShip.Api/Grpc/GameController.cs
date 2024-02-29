using BattleShip.Api.Services;
using BattleShip.Models;
using Grpc.Core;

namespace BattleShip.Api.Grpc;

public class BattleShipServiceImpl : BattleShipService.BattleShipServiceBase
{
    private readonly GameService _gameService;

    public BattleShipServiceImpl(GameService gameService)
    {
        _gameService = gameService;
    }

    public override Task<InitializeGameResponse> CreateGame(InitializeGameRequest request, ServerCallContext context)
    {
        var guid = new Guid(request.CreatorId);
        var gameSettings =
            new BattleShip.Models.GameSettings((GameMode)request.GameSettings.Mode,
                (AiDifficulty?)request.GameSettings.Difficulty);

        var gameInfo = _gameService.InitializeGame(guid, gameSettings);

        var ships = gameInfo.Ships.Select(s => new Ship
        {
            X = s.X,
            Y = s.Y,
            Direction = (int)s.Direction,
            Type = (int)s.Type
        }).ToArray();
        var response = new InitializeGameResponse
        {
            SessionId = gameInfo.SessionId.ToString(),
            Player1Id = gameInfo.Player1Id.ToString(),
            Ships = { ships },
            Status = (int)gameInfo.Status
        };
        return Task.FromResult(response);
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