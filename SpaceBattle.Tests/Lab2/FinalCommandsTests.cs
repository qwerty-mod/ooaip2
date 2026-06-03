using Moq;
using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Tests.Lab2;

public class FinalCommandsTests
{
    [Fact]
    public void Execute_RotateWithVelocity_UpdatesDirectionAndVelocity()
    {
        // Arrange
        var dummyObj = new object();
        
        var velocityMock = new Mock<IVelocityChangeable>();
        velocityMock.SetupProperty(v => v.Velocity, new Vector(1, 1));

        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(r => r.Direction, 0);
        rotatableMock.SetupGet(r => r.AngularVelocity).Returns(2);
        rotatableMock.SetupGet(r => r.DirectionsCount).Returns(8);

        // Регистрируем адаптеры в IoC
        IoC.Resolve<object>("IoC.Register", "Adapters.IVelocityChangeableObject", (Func<object[], object>)(args => velocityMock.Object));
        IoC.Resolve<object>("IoC.Register", "Adapters.IRotatingObject", (Func<object[], object>)(args => rotatableMock.Object));

        // Регистрируем финальную макрокоманду
        var registerFinal = new RegisterIoCDependencyRotateWithVelocityCommand();
        registerFinal.Execute();

        // Act
        var macroCmd = IoC.Resolve<ICommand>("Commands.Macro.RotateWithVelocity", dummyObj);
        macroCmd.Execute();

        // Assert
        Assert.Equal(2, rotatableMock.Object.Direction);
        Assert.NotNull(velocityMock.Object.Velocity);
    }
}