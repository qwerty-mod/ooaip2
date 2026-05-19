using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab4;

public class CollisionDetector
{
    // теперь храним только ОТНОСИТЕЛЬНЫЕ параметры (смещение и разность скоростей)
    private readonly HashSet<(Vector relativePos, Vector relativeVel)> _collisionData;

    public CollisionDetector(HashSet<(Vector, Vector)> collisionData)
    {
        _collisionData = collisionData ?? throw new ArgumentNullException(nameof(collisionData));
    }

    public bool IsCollision(ICollisionObject obj1, ICollisionObject obj2)
    {
        if (obj1 == null || obj2 == null) return false;

        // вычисляем относительную позицию и скорость двух тел
        // математически: DeltaP = P1 - P2, DeltaV = V1 - V2
        // используем IoC для вычитания векторов, чтобы не зависеть от внутренней реализации Vector
        var deltaPos = IoC.Resolve<Vector>("Math.Vector.Subtract", obj1.Position, obj2.Position);
        var deltaVel = IoC.Resolve<Vector>("Math.Vector.Subtract", obj1.Velocity, obj2.Velocity);

        // проверка за O(1) инварианта
        return _collisionData.Contains((deltaPos, deltaVel));
    }
}