namespace SpaceBattle.Lib.Lab3;

public interface IAuthService
{
    bool Authorize(string playerId, string objectId);
}