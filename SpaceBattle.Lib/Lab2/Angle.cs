namespace SpaceBattle.Lib.Lab2;

public class Angle
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Angle(int numerator, int denominator)
    {
        if (denominator <= 0) throw new ArgumentException("знаменатель должен быть больше нуля");
        Denominator = denominator;
        // нормализуем угол чтобы он всегда был в диапазоне [0, denominator)
        int mod = numerator % denominator;
        Numerator = mod < 0 ? mod + denominator : mod;
    }

    public static Angle operator +(Angle a1, Angle a2)
    {
        if (a1.Denominator != a2.Denominator)
            throw new ArgumentException("знаменатели углов должны совпадать");
        return new Angle(a1.Numerator + a2.Numerator, a1.Denominator);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Angle other) return false;
        return Numerator == other.Numerator && Denominator == other.Denominator;
    }

    public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);

    public static bool operator ==(Angle? a1, Angle? a2)
    {
        if (ReferenceEquals(a1, a2)) return true;
        if (a1 is null || a2 is null) return false;
        return a1.Equals(a2);
    }

    public static bool operator !=(Angle? a1, Angle? a2) => !(a1 == a2);
}