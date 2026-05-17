namespace SpaceBattle.Lib.Lab2;

public class RotateCommand : ICommand
{
    private readonly IRotatable _rotatable;

    public RotateCommand(IRotatable rotatable)
    {
        _rotatable = rotatable ?? throw new ArgumentNullException(nameof(rotatable));
    }

    public void Execute()
    {
        try
        {
            // новый угол = (текущий + скорость) % общее кол-во направлений
            _rotatable.Direction = (_rotatable.Direction + _rotatable.AngularVelocity) % _rotatable.DirectionsCount;
        }
        catch (Exception ex)
        {
            throw new Exception("невозможно выполнить команду поворота", ex);
        }
    }
}