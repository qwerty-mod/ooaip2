namespace SpaceBattle.Lib.Lab2;

public class RegisterIoCDependencyRotateWithVelocityCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<object>(
            "IoC.Register",
            "Commands.Macro.RotateWithVelocity",
            (Func<object[], object>)(args =>
            {
                var obj = args[0];
                
                var velocityChangeable = IoC.Resolve<IVelocityChangeable>("Adapters.IVelocityChangeableObject", obj);
                var rotatable = IoC.Resolve<IRotatable>("Adapters.IRotatingObject", obj);

                var rotateCmd = new RotateCommand(rotatable);
                var changeVelocityCmd = new ChangeVelocityCommand(velocityChangeable, rotatable);

                return new MacroCommand(new ICommand[] { rotateCmd, changeVelocityCmd });
            })
        );
    }
}