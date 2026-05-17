namespace SpaceBattle.Lib.Lab2;

public class MoveCommand : ICommand
{
    private readonly IMovable _movable;

    public MoveCommand(IMovable movable)
    {
        _movable = movable ?? throw new ArgumentNullException(nameof(movable));
    }

    public void Execute()
    {
        try
        {
            _movable.Position = _movable.Position + _movable.Velocity;
        }
        catch (Exception ex)
        {
            // Если упало при чтении/записи координат или сложении векторов - кидаем Exception 
            throw new Exception("невозможно выполнить команду перемещения", ex);
        }
    }
}