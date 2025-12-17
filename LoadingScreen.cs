using Godot;
using System;

public partial class LoadingScreen : Control
{
    public override void _Ready()
    {
        EventBus.RegisterCallBack("show_loading_screen", new Callable(this,nameof(showLoadingScreen)));
        var tween=GetTree().CreateTween();
        tween.TweenProperty(this, "modulate", Color.Color8(255,255,255,0), 1);
    }
    public void showLoadingScreen()
    {
        Modulate = Color.Color8(255, 255, 255, 255);
    }
}
