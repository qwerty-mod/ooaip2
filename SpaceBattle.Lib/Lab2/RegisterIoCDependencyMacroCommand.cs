namespace SpaceBattle.Lib.Lab2;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    private readonly string _macroKey;
    private readonly string[] _subCommandKeys;

    // конструктор принимает ключ макрокоманды и список ключей подкоманд, из которых она состоит
    public RegisterIoCDependencyMacroCommand(string macroKey, params string[] subCommandKeys)
    {
        _macroKey = macroKey;
        _subCommandKeys = subCommandKeys;
    }

    public void Execute()
    {
        IoC.Resolve<object>(
            "IoC.Register",
            _macroKey,
            (Func<object[], object>)(args =>
            {
                var obj = args[0]; // Игровой объект
                var list = new List<ICommand>();

                // сброка всех подкоманд через IoC для этого объекта
                foreach (var key in _subCommandKeys)
                {
                    var cmd = IoC.Resolve<ICommand>(key, obj);
                    list.Add(cmd);
                }

                return new MacroCommand(list);
            })
        );
    }
}