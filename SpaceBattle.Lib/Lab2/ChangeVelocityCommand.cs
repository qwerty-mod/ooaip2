namespace SpaceBattle.Lib.Lab2;

public class ChangeVelocityCommand : ICommand
{
    private readonly IVelocityChangeable _velocityChangeable;
    private readonly IRotatable _rotatable;

    public ChangeVelocityCommand(IVelocityChangeable velocityChangeable, IRotatable rotatable)
    {
        _velocityChangeable = velocityChangeable ?? throw new ArgumentNullException(nameof(velocityChangeable));
        _rotatable = rotatable ?? throw new ArgumentNullException(nameof(rotatable));
    }

    public void Execute()
    {
        try
        {
            int[] newCoords;

            if (_velocityChangeable.Velocity.Size == 2)
            {
                newCoords = new int[] { -_velocityChangeable.Velocity.Size, _rotatable.Direction };
            }
            else
            {
                newCoords = new int[] { _rotatable.Direction };
            }

            _velocityChangeable.Velocity = new Vector(newCoords);
        }
        catch (Exception ex)
        {
            throw new Exception("Невозможно изменить вектор скорости.", ex);
        }
    }
}