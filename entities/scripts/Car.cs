using Godot;
using System;

public partial class Car : Vehicule
{
    public Car(Direction direction,float speed)
    {
        Speed = speed*(int)direction;
    }
    public Car() { }

    public override void _Ready()
    {
        base._Ready();
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
