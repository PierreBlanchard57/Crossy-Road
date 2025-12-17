using Godot;
using System;

public partial class ScoreLabel : Label
{
    public override void _Ready()
    {
        EventBus.RegisterCallBack("score_adding", new Callable(this, nameof(addScore)));
    }
    public void addScore(int score)
    {
        var finalScore=Text.ToInt() + score;
        Text=finalScore.ToString();
    }
}
