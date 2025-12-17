using Godot;
using System;
using System.Threading.Tasks;

public partial class ReplayButton : TextureButton
{
    public override void _Ready()
    {
        EventBus.RegisterCallBack("spawn_replay_button",new Callable(this,nameof(spawnButton)));
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("click") && Game.GameState==Game.GameStates.GAMEENDED)
        {
            var pos=GetWindow().GetMousePosition();
            if (pos.X>=Position.X && pos.X<=Position.X+Size.X)
            {
                if(pos.Y>=Position.Y && pos.Y <= Position.Y + Size.Y)
                {
                    _Pressed();
                }
            }
        }
    }
    public override void _Pressed()
    {
        
        Game.GeneratedTiles = 12;
        Game.PlayerProgress = 5;
        EventBus.Emit("show_loading_screen", null);
        
        EventBus.Reset();
        var newScene = ResourceLoader.Load<PackedScene>("res://Game.tscn").Instantiate();
        var timer = GetTree().CreateTimer(0.5);
        timer.Timeout += () =>
        {
            var currentScene = GetTree().CurrentScene;
            GetTree().Root.AddChild(newScene);
            GetTree().CurrentScene = newScene;
            currentScene.QueueFree();
            Game.GameState = Game.GameStates.MAINMENU;
        };
        

    }
    public void spawnButton()
    {
        Visible = true;
        var pos = Position;
        Position=new Vector2(Position.X, -50);
        var tween=GetTree().CreateTween();
        tween.TweenProperty(this, "position", pos, 0.4);

    } 
}
