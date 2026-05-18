using Moq;
using SpaceBattle.Lib.Lab2;
using SpaceBattle.Lib.Lab3;

namespace SpaceBattle.Tests.Lab3;

public class ShootCommandTests
{
    [Fact]
    public void Execute_ValidShooter_CreatesAndRegistersTorpedo()
    {
        // Arrange
        var shooterMock = new Mock<IShooter>();
        shooterMock.SetupGet(s => s.Position).Returns(new Vector(1, 2));
        shooterMock.SetupGet(s => s.Direction).Returns(new Vector(0, 1));
        shooterMock.SetupGet(s => s.TorpedoId).Returns("torpedo_1");

        var repoMock = new Mock<IRepository<object>>();
        var dummyTorpedo = new object();

        // регистрируем в IoC фейковую стратегию создания торпеды для теста
        IoC.Resolve<object>("IoC.Register", "Game.CreateTorpedo", (Func<object[], object>)(args => dummyTorpedo));

        var shootCmd = new ShootCommand(shooterMock.Object, repoMock.Object);

        // Act
        shootCmd.Execute();

        // Assert
        repoMock.Verify(r => r.Add("torpedo_1", dummyTorpedo), Times.Once);
    }

    [Fact]
    public void Execute_IoCFails_ThrowsException()
    {
        // Arrange
        var shooterMock = new Mock<IShooter>();
        var repoMock = new Mock<IRepository<object>>();

        IoC.Resolve<object>("IoC.Register", "Game.CreateTorpedo", (Func<object[], object>)(args => throw new Exception()));

        var shootCmd = new ShootCommand(shooterMock.Object, repoMock.Object);

        // Act & Assert
        Assert.Throws<Exception>((Action)(() => shootCmd.Execute()));
    }

    [Fact]
    public void Game_Update_ExecutesInjectedCommand()
    {
        // Arrange
        var repoMock = new Mock<IRepository<object>>();
        var game = new Game(repoMock.Object);
        var cmdMock = new Mock<ICommand>();

        game.AddCommand(cmdMock.Object);

        // Act
        game.Update();

        // Assert
        cmdMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void AuthCommand_ValidCredentials_ExecutesTargetCommand()
    {
        // Arrange
        var authMock = new Mock<IAuthService>();
        authMock.Setup(a => a.Authorize("player1", "ship1")).Returns(true);

        var cmdMock = new Mock<ICommand>();
        var authCmd = new AuthCommand("player1", "ship1", authMock.Object, cmdMock.Object);

        // Act
        authCmd.Execute();

        // Assert
        cmdMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void AuthCommand_InvalidCredentials_ThrowsUnauthorizedException()
    {
        // Arrange
        var authMock = new Mock<IAuthService>();
        authMock.Setup(a => a.Authorize("player1", "ship1")).Returns(false);

        var cmdMock = new Mock<ICommand>();
        var authCmd = new AuthCommand("player1", "ship1", authMock.Object, cmdMock.Object);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>((Action)(() => authCmd.Execute()));
        cmdMock.Verify(c => c.Execute(), Times.Never);
    }
}