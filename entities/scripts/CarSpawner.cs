using CrossyRoad;
using Godot;
using System;
using System.Collections.Generic;

public partial class CarSpawner : Spawner
{
    public override void _Ready()
    {
       base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
    }
}
