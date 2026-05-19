using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab4;

public class CheckCollisionCommand : ICommand
{
    private readonly ICollisionObject _obj1;
    private readonly ICollisionObject _obj2;
    private readonly CollisionDetector _detector;
    private readonly Action _collisionHandler;

    public CheckCollisionCommand(ICollisionObject obj1, ICollisionObject obj2, CollisionDetector detector, Action collisionHandler)
    {
        _obj1 = obj1 ?? throw new ArgumentNullException(nameof(obj1));
        _obj2 = obj2 ?? throw new ArgumentNullException(nameof(obj2));
        _detector = detector ?? throw new ArgumentNullException(nameof(detector));
        _collisionHandler = collisionHandler ?? throw new ArgumentNullException(nameof(collisionHandler));
    }

    public void Execute()
    {
        if (_detector.IsCollision(_obj1, _obj2))
        {
            _collisionHandler.Invoke(); // вызываем обработчик столкновения (например: взрыв торпеды/корабля)
        }
    }
}