using Godot;
using System;

public partial class Train : Vehicule
{
    public Train(Direction direction, float speed)
    {
        Speed = speed * (int)direction;
    }
    public Train() { }

    public override void _Ready()
    {
        base._Ready();
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }
}
