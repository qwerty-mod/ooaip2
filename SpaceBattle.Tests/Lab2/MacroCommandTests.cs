using Moq;
using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Tests.Lab2;

public class MacroCommandTests
{
    [Fact]
    public void Execute_AllCommandsSuccess_RunsEveryCommand()
    {
        // Arrange
        var cmdMock1 = new Mock<ICommand>();
        var cmdMock2 = new Mock<ICommand>();
        
        var macro = new MacroCommand(new[] { cmdMock1.Object, cmdMock2.Object });

        // Act
        macro.Execute();

        // Assert
        cmdMock1.Verify(c => c.Execute(), Times.Once);
        cmdMock2.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_CommandFails_ThrowsExceptionAndStops()
    {
        // Arrange
        var cmdMock1 = new Mock<ICommand>();
        var cmdMock2 = new Mock<ICommand>();
        
        // Первая команда бросает ошибку
        cmdMock1.Setup(c => c.Execute()).Throws<Exception>();

        var macro = new MacroCommand(new[] { cmdMock1.Object, cmdMock2.Object });

        // Act & Assert
        Assert.Throws<Exception>((Action)(() => macro.Execute()));
        
        // Вторая команда не должна была выполниться так как на первой всё упало
        cmdMock2.Verify(c => c.Execute(), Times.Never);
    }

    [Fact]
    public void IoC_RegisterAndResolveMacroCommand_ReturnsMacroCommand()
    {
        // Arrange
        var dummyObj = new object();
        var cmdMock = new Mock<ICommand>();

        // Регистрируем какую-нибудь фейковую простую команду в IoC
        IoC.Resolve<object>("IoC.Register", "Commands.SubTask", (Func<object[], object>)(args => cmdMock.Object));

        // Регистрируем макрокоманду состоящую из одной подкоманды Commands.SubTask
        var registerMacro = new RegisterIoCDependencyMacroCommand("Commands.Macro.Test", "Commands.SubTask");
        registerMacro.Execute();

        // Act
        var resolvedMacro = IoC.Resolve<ICommand>("Commands.Macro.Test", dummyObj);

        // Assert
        Assert.NotNull(resolvedMacro);
        Assert.IsType<MacroCommand>(resolvedMacro);
    }
}