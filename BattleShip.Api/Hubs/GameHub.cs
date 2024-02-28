using Microsoft.AspNetCore.SignalR;

namespace BattleShip.Api.Hubs;

public class GameHub : Hub
{
    private static readonly Dictionary<string, string> PlayerConnections = new();
}