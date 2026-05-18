using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab3;

public interface IShooter
{
    Vector Position { get; }
    Vector Direction { get; }
    string TorpedoId { get; }
}