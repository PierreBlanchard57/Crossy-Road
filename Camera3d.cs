using Godot;
using System;

public partial class Camera3d : Camera3D
{
    public override void _Ready()
    {
        EventBus.RegisterCallBack("player_moving",new Callable(this,nameof(MoveCamera)));
    }
    public void MoveCamera(Vector3 vector3)
    {
        Vector3 final = Position + vector3;
        if (final.Z >= 1)
        {
            Tween tween = GetTree().CreateTween();
            tween.TweenProperty(this, "position", final, 0.15);
        }
        
    }
}
