using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Lib.Lab3;

public class AuthCommand : ICommand
{
    private readonly string _playerId;
    private readonly string _objectId;
    private readonly IAuthService _authService;
    private readonly ICommand _targetCommand;

    public AuthCommand(string playerId, string objectId, IAuthService authService, ICommand targetCommand)
    {
        _playerId = playerId;
        _objectId = objectId;
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _targetCommand = targetCommand ?? throw new ArgumentNullException(nameof(targetCommand));
    }

    public void Execute()
    {
        // если проверка провалена - выбрасываем исключение безопасности
        if (!_authService.Authorize(_playerId, _objectId))
        {
            throw new UnauthorizedAccessException($"игрок '{_playerId}' не имеет прав на управление объектом '{_objectId}'");
        }

        // если всё ок- выполняем вложенную команду (например ShootCommand)
        _targetCommand.Execute();
    }
}