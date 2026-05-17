using SpaceBattle.Lib.Lab2;

namespace SpaceBattle.Tests.Lab2;

public class VectorTests
{
    [Fact]
    public void SumOfOppositeVectors_ReturnsZeroVector()
    {
        var v1 = new Vector(1, -1, 2);
        var v2 = new Vector(-1, 1, -2);
        var expected = new Vector(0, 0, 0);

        Assert.Equal(expected, v1 + v2);
    }

    [Fact]
    public void SumOfDifferentSizeVectors_FirstLonger_ThrowsArgumentException()
    {
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(1, 2);

        Assert.Throws<ArgumentException>(() => v1 + v2);
    }

    [Fact]
    public void SumOfDifferentSizeVectors_SecondLonger_ThrowsArgumentException()
    {
        var v1 = new Vector(1, 2);
        var v2 = new Vector(1, 2, 3);

        Assert.Throws<ArgumentException>(() => v1 + v2);
    }

    [Fact]
    public void Equals_SameCoordinates_ReturnsTrue()
    {
        var v1 = new Vector(5, 10);
        var v2 = new Vector(5, 10);

        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void OperatorEquals_SameCoordinates_ReturnsTrue()
    {
        var v1 = new Vector(5, 10);
        var v2 = new Vector(5, 10);

        Assert.True(v1 == v2);
    }

    [Fact]
    public void Equals_DifferentCoordinates_ReturnsFalse()
    {
        var v1 = new Vector(5, 10);
        var v2 = new Vector(5, 11);

        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void OperatorNotEquals_DifferentCoordinates_ReturnsTrue()
    {
        var v1 = new Vector(5, 10);
        var v2 = new Vector(5, 11);

        Assert.True(v1 != v2);
    }

    [Fact]
    public void GetHashCode_SameCoordinates_ReturnsSameHash()
    {
        var v1 = new Vector(3, 4, 5);
        var v2 = new Vector(3, 4, 5);

        Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
    }
}