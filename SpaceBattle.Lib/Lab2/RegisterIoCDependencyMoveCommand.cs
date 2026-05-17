namespace SpaceBattle.Lib.Lab2;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<object>(
            "IoC.Register",
            "Commands.Move",
            (Func<object[], object>)(args =>
            {
                var obj = args[0];
                var movableAdapter = IoC.Resolve<IMovable>("Adapters.IMovingObject", obj);
                return new MoveCommand(movableAdapter);
            })
        );
    }
}