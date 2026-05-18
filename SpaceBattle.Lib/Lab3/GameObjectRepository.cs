namespace SpaceBattle.Lib.Lab3;

public class GameObjectRepository : IRepository<object>
{
    private readonly Dictionary<string, object> _storage = new();

    public object GetById(string id)
    {
        if (!_storage.TryGetValue(id, out var obj))
        {
            throw new KeyNotFoundException($"объект с id '{id}' не найден");
        }
        return obj;
    }

    public void Add(string id, object obj)
    {
        if (_storage.ContainsKey(id))
        {
            throw new ArgumentException($"объект с id '{id}' уже существует");
        }
        _storage[id] = obj;
    }
}