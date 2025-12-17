using Godot;
using System;

public partial class CoinLabel : Label
{
    int coins = 0;
    public override void _Ready()
    {
        EventBus.RegisterCallBack("coin_collected",new Callable(this,nameof(addCoins)));
    }
    public void addCoins(int coins)
    {
        this.coins+=coins;
        this.Text = this.coins.ToString();
    }
}
