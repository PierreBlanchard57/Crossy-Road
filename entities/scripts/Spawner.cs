using CrossyRoad;
using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Area3D
{
    [Export] protected Vehicule.Direction Direction { get; set; } = Vehicule.Direction.LEFT;
    [Export] protected float Speed { get; set; }
    [Export] protected bool isSpeedRandom { get; set; }
    [Export] protected PackedScene spawnable;
    CyclicEvent spawnEvent;

    public override void _Ready()
    {
        spawnEvent = new CyclicEvent(60, 120, new Callable(this, nameof(spawn)));
        if (isSpeedRandom)
        {
            var rng = new RandomNumberGenerator();
            Speed = (float)rng.RandfRange(Vehicule.MINSPEED, Vehicule.MAXSPEED);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        //Spawn only if player is below 10 blocs
        if (Game.PlayerProgress - Position.Z < 10)
        {
            spawnEvent.stepOneFrame();
        }

    }
    public virtual void spawn()
    {
        Vehicule vehicle = (Vehicule)spawnable.Instantiate();

        vehicle.Speed = Speed * (int)Direction;
        AddChild(vehicle);
    }
}
