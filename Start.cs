using Godot;
using System;

public partial class Start : TextureRect
{
    public override void _PhysicsProcess(double delta)
    {
        if(Game.GameState==Game.GameStates.MAINMENU  && Input.IsActionJustPressed("click")){
            Game.GameState = Game.GameStates.INGAME;
            var tween=GetTree().CreateTween();
            var pos = new Vector2(Position.X, -150);
            tween.TweenProperty(this, "position", pos, 0.2);
        }
    }
}
