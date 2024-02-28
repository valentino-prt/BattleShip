namespace BattleShip.Api.Utils;

public class ConnectionMapping
{
    private readonly Dictionary<Guid, string> _connections = new();

    public void Add(Guid userId, string connectionId)
    {
        lock (_connections)
        {
            _connections[userId] = connectionId;
        }
    }

    public void Remove(Guid userId)
    {
        lock (_connections)
        {
            _connections.Remove(userId);
        }
    }

    public string GetConnectionId(Guid userId)
    {
        lock (_connections)
        {
            _connections.TryGetValue(userId, out var connectionId);
            return connectionId;
        }
    }
}