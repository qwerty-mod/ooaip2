using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab4;

public class MoveWithCollisionCheckCommand : ICommand
{
    private readonly ICommand _checkCollisionCommand;
    private readonly ICommand _moveCommand;

    public MoveWithCollisionCheckCommand(ICommand checkCollisionCommand, ICommand moveCommand)
    {
        _checkCollisionCommand = checkCollisionCommand;
        _moveCommand = moveCommand;
    }

    public void Execute()
    {
        // сначала проверяем на коллизии с окружающими объектами
        _checkCollisionCommand.Execute();
        // если исключения не возникло (или обработчик не остановил игру) то двигаем объект
        _moveCommand.Execute();
    }
}