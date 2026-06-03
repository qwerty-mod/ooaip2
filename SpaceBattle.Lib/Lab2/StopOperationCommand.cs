using System.Collections.Generic;

namespace SpaceBattle.Lib.Lab2;

public class StopOperationCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StopOperationCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var injectable = (ICommandInjectable)_order["TargetCommand"];
        var empty = IoC.Resolve<ICommand>("Commands.Empty");
        
        // gодменяем команду на пустую за O(1)
        injectable.Inject(empty);
    }
}