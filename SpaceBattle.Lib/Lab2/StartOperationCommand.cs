using System.Collections.Generic;

namespace SpaceBattle.Lib.Lab2;

public class StartOperationCommand : ICommand
{
    private readonly IDictionary<string, object> _order;

    public StartOperationCommand(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var obj = _order["Object"];
        var operationName = (string)_order["Operation"];
        
        // разрешаем саму команду (например - Commands.Move) через IoC
        var command = IoC.Resolve<ICommand>(operationName, obj);
        
        // получаем очередь сервера
        var queue = IoC.Resolve<ICommandReceiver>("Game.Queue");
        
        // отправляем команду в обработку
        queue.Receive(command);
    }
}