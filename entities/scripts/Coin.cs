using Godot;
using System;

public partial class Coin : Area3D
{
    public override void _Ready()
    {
        this.BodyEntered += Coin_BodyEntered;
    }

    private void Coin_BodyEntered(Node3D body)
    {
        if (body.GetType() == typeof(Player))
        {
            EventBus.Emit("coin_collected", 1);
            GetParent().QueueFree();
        }
    }
}
