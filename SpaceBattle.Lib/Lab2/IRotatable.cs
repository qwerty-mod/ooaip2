namespace SpaceBattle.Lib.Lab2;

public interface IRotatable
{
    int Direction { get; set; }
    int AngularVelocity { get; }
    int DirectionsCount { get; } // всего дискретных направлений
}