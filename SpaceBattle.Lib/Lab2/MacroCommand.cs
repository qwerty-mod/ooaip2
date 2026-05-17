namespace SpaceBattle.Lib.Lab2;

public class MacroCommand : ICommand
{
    private readonly IEnumerable<ICommand> _commands;

    public MacroCommand(IEnumerable<ICommand> commands)
    {
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public void Execute()
    {
        try
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при выполнении макрокоманды", ex);
        }
    }
}