using Moq;
using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Tests.Lab2;

public class MoveCommandTests
{
    [Fact]
    public void Move_ValidPositionAndVelocity_UpdatesPositionCorrectly()
    {
        var movableMock = new Mock<IMovable>();
        movableMock.SetupProperty(m => m.Position, new Vector(12, 5));
        movableMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));

        var moveCommand = new MoveCommand(movableMock.Object);

        moveCommand.Execute();

        var expectedPosition = new Vector(8, 6);
        Assert.Equal(expectedPosition, movableMock.Object.Position);
    }

    [Fact]
    public void Move_CannotGetPosition_ThrowsException()
    {
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Position).Throws<Exception>();
        movableMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));

        var moveCommand = new MoveCommand(movableMock.Object);

        Assert.Throws<Exception>((Action)(() => moveCommand.Execute()));
    }

    [Fact]
    public void Move_CannotGetVelocity_ThrowsException()
    {
        var movableMock = new Mock<IMovable>();
        movableMock.SetupProperty(m => m.Position, new Vector(12, 5));
        movableMock.SetupGet(m => m.Velocity).Throws<Exception>();

        var moveCommand = new MoveCommand(movableMock.Object);

        Assert.Throws<Exception>((Action)(() => moveCommand.Execute()));
    }

    [Fact]
    public void Move_CannotSetPosition_ThrowsException()
    {
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Position).Returns(new Vector(12, 5));
        movableMock.SetupGet(m => m.Velocity).Returns(new Vector(-4, 1));
        movableMock.SetupSet(m => m.Position = It.IsAny<Vector>()).Throws<Exception>();

        var moveCommand = new MoveCommand(movableMock.Object);

        Assert.Throws<Exception>((Action)(() => moveCommand.Execute()));
    }
}