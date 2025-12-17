using Godot;
using System;

public partial class TrainSpawner : Spawner
{
    [Export] int trainLength { get; set; } = 5;
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

    }
    public override void spawn()
    {
        for (int i = 0; i < trainLength; i++)
        {
            Vehicule vehicle = (Vehicule)spawnable.Instantiate();
            AddChild(vehicle);
            vehicle.Speed = Speed * (int)Direction;
            vehicle.Position += new Vector3(i* (int)Direction, 0,0); 
        }
    }
}
