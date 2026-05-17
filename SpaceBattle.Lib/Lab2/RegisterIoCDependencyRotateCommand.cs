namespace SpaceBattle.Lib.Lab2;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<object>(
            "IoC.Register",
            "Commands.Rotate",
            (Func<object[], object>)(args =>
            {
                var obj = args[0];
                var rotatableAdapter = IoC.Resolve<IRotatable>("Adapters.IRotatingObject", obj);
                return new RotateCommand(rotatableAdapter);
            })
        );
    }
}