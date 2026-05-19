using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab4;

public interface ICollisionObject
{
    Vector Position { get; }
    Vector Velocity { get; }
}