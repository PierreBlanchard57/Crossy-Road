using Godot;
using System;

public partial class Vehicule : Area3D
{
    public static float MINSPEED = 0.05f;
    public static float MAXSPEED = 0.15f;
    public float Speed { get; set; } = 0.05f;
    public enum Direction
    {
        LEFT = 1,
        RIGHT = -1
    }
    public Vehicule() { }
    public Vehicule(Direction direction, float speed)
    {
        Speed = speed * (int)direction;
    }

    public override void _Ready()
    {
        BodyEntered += bodyEntered;
        var childCount = GetChildCount();
        MeshInstance3D mesh = (MeshInstance3D)GetChild(new Random().Next(0, childCount - 1));
        mesh.Visible = true;
    }
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += new Vector3(Speed, 0, 0);
        if (GlobalPosition.X > 30 || GlobalPosition.X < -35) QueueFree();
    }

    private void bodyEntered(Node3D body)
    {
        if (body.GetType() == typeof(Player))
        {
            if (Game.GameState == Game.GameStates.INGAME) ((Player)body).flatPlayer();
        }
    }
}
