namespace SpaceBattle.Lib.Lab2;

public class Vector
{
    private readonly int[] _coordinates;

    public Vector(params int[] coordinates)
    {
        _coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
    }

    public int Size => _coordinates.Length;

    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size)
        {
            throw new ArgumentException("векторы должны быть одинаковой размерности");
        }

        int[] result = new int[v1.Size];
        for (int i = 0; i < v1.Size; i++)
        {
            result[i] = v1._coordinates[i] + v2._coordinates[i];
        }

        return new Vector(result);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Vector other || Size != other.Size)
        {
            return false;
        }

        for (int i = 0; i < Size; i++)
        {
            if (_coordinates[i] != other._coordinates[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator ==(Vector? v1, Vector? v2)
    {
        if (ReferenceEquals(v1, v2)) return true;
        if (v1 is null || v2 is null) return false;
        return v1.Equals(v2);
    }

    public static bool operator !=(Vector? v1, Vector? v2)
    {
        return !(v1 == v2);
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var coord in _coordinates)
        {
            hash = hash * 23 + coord.GetHashCode();
        }
        return hash;
    }
}