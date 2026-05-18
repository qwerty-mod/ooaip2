using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab3;

public class ShootCommand : ICommand
{
    private readonly IShooter _shooter;
    private readonly IRepository<object> _repository;

    public ShootCommand(IShooter shooter, IRepository<object> repository)
    {
        _shooter = shooter ?? throw new ArgumentNullException(nameof(shooter));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void Execute()
    {
        try
        {
            // создаем торпеду через наш IoC передавая ей позицию и направление стрелка
            var torpedo = IoC.Resolve<object>("Game.CreateTorpedo", _shooter.Position, _shooter.Direction);
            
            // регистрируем торпеду в хранилище игры
            _repository.Add(_shooter.TorpedoId, torpedo);
        }
        catch (Exception ex)
        {
            throw new Exception("не удалось выполнить выстрел", ex);
        }
    }
}