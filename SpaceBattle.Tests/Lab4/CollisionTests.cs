using Moq;
using SpaceBattle.Lib.Lab2;
using SpaceBattle.Lib.Lab4;

namespace SpaceBattle.Tests.Lab4;

public class CollisionTests
{
    public CollisionTests()
    {
        // регистрируем в IoC фейковое вычитание векторов для тестов чтобы код не падал
        IoC.Resolve<object>("IoC.Register", "Math.Vector.Subtract", (Func<object[], object>)(args => {
            var v1 = (Vector)args[0];
            var v2 = (Vector)args[1];
            // имитируем разность векторов для теста (возвращаем фиксированный вектор разности)
            return new Vector(10, 20); 
        }));
    }

    [Fact]
    public void Execute_CollisionDetected_InvokesHandler()
    {
        // Arrange
        var obj1Mock = new Mock<ICollisionObject>();
        obj1Mock.SetupGet(o => o.Position).Returns(new Vector(30, 50));
        obj1Mock.SetupGet(o => o.Velocity).Returns(new Vector(5, 5));

        var obj2Mock = new Mock<ICollisionObject>();
        obj2Mock.SetupGet(o => o.Position).Returns(new Vector(20, 30));
        obj2Mock.SetupGet(o => o.Velocity).Returns(new Vector(3, 3));

        // относительные параметры которые вернет наш IoC "Math.Vector.Subtract" (10, 20)
        var expectedDeltaPos = new Vector(10, 20);
        var expectedDeltaVel = new Vector(10, 20); // Наш мок IoC возвращает (10,20)

        var collisionData = new HashSet<(Vector, Vector)> { (expectedDeltaPos, expectedDeltaVel) };
        var detector = new CollisionDetector(collisionData);

        bool handlerInvoked = false;
        Action handler = () => { handlerInvoked = true; };

        var command = new CheckCollisionCommand(obj1Mock.Object, obj2Mock.Object, detector, handler);

        // Act
        command.Execute();

        // Assert
        Assert.True(handlerInvoked); 
    }

    [Fact]
    public void Execute_NoCollision_DoesNotInvokeHandler()
    {
        // Arrange
        var obj1Mock = new Mock<ICollisionObject>();
        var obj2Mock = new Mock<ICollisionObject>();

        // пустая база относительных коллизий
        var collisionData = new HashSet<(Vector, Vector)>();
        var detector = new CollisionDetector(collisionData);

        bool handlerInvoked = false;
        Action handler = () => { handlerInvoked = true; };

        var command = new CheckCollisionCommand(obj1Mock.Object, obj2Mock.Object, detector, handler);

        // Act
        command.Execute();

        // Assert
        Assert.False(handlerInvoked); 
    }
}