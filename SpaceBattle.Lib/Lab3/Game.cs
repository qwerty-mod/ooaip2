using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab3;

public class Game
{
    private readonly IRepository<object> _repository;
    private readonly Queue<ICommand> _commandQueue = new();

    public Game(IRepository<object> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IRepository<object> Repository => _repository;

    // метод добавления команды в игровой цикл
    public void AddCommand(ICommand command)
    {
        _commandQueue.Enqueue(command);
    }

    // один шаг игры: обрабатываем одну команду из очереди
    public void Update()
    {
        if (_commandQueue.Count > 0)
        {
            var command = _commandQueue.Dequeue();
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                // по ТЗ игра должна управлять состоянием поэтому логируем или обрабатываем сбои
                throw new Exception("ошибка при обработке игрового такта", ex);
            }
        }
    }
}