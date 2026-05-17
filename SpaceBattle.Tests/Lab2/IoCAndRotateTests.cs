using Moq;
using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Tests.Lab2;

public class IoCAndRotateTests
{
    [Fact]
    public void IoC_RegisterAndResolveMove_ReturnsMoveCommand()
    {
        var dummyObj = new object();
        var movableMock = new Mock<IMovable>();

        IoC.Resolve<object>("IoC.Register", "Adapters.IMovingObject", (Func<object[], object>)(args => movableMock.Object));
        
        var registerMove = new RegisterIoCDependencyMoveCommand();
        registerMove.Execute();

        var command = IoC.Resolve<ICommand>("Commands.Move", dummyObj);

        Assert.NotNull(command);
        Assert.IsType<MoveCommand>(command);
    }

    [Fact]
    public void Rotate_ValidDirection_UpdatesCorrectly()
    {
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupProperty(r => r.Direction, 2);
        rotatableMock.SetupGet(r => r.AngularVelocity).Returns(2);
        rotatableMock.SetupGet(r => r.DirectionsCount).Returns(8);

        var rotateCmd = new RotateCommand(rotatableMock.Object);
        rotateCmd.Execute();

        Assert.Equal(4, rotatableMock.Object.Direction);
    }

    [Fact]
    public void Rotate_ExceptionWhenCannotGetDirection_ThrowsException()
    {
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.Direction).Throws<Exception>();

        var rotateCmd = new RotateCommand(rotatableMock.Object);

        Assert.Throws<Exception>((Action)(() => rotateCmd.Execute()));
    }

    [Fact]
    public void IoC_RegisterAndResolveRotate_ReturnsRotateCommand()
    {
        var dummyObj = new object();
        var rotatableMock = new Mock<IRotatable>();

        IoC.Resolve<object>("IoC.Register", "Adapters.IRotatingObject", (Func<object[], object>)(args => rotatableMock.Object));
        
        var registerRotate = new RegisterIoCDependencyRotateCommand();
        registerRotate.Execute();

        var command = IoC.Resolve<ICommand>("Commands.Rotate", dummyObj);

        Assert.NotNull(command);
        Assert.IsType<RotateCommand>(command);
    }
}