using BattleShip.Api.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BattleShip.Api.Hubs;

public class PlayerHub : Hub
{
    private readonly ConnectionMapping _connectionMapping;

    public PlayerHub(ConnectionMapping connectionMapping)
    {
        _connectionMapping = connectionMapping;
    }

    public async Task RegisterUserId(Guid userId)
    {
        var connectionId = Context.ConnectionId;
        _connectionMapping.Add(userId, connectionId);
    }
}