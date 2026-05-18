namespace SpaceBattle.Lib.Lab3;

public interface IRepository<T>
{
    T GetById(string id);
    void Add(string id, T obj);
}