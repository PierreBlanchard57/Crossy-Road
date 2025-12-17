using Godot;
using System;

public partial class Game : Node3D
{
    [Export] GridMap gridmap;
    public static int GeneratedTiles { get; set; } = 12;
    public static int GenerationDistance { get; set; } = 10;
    public static int PlayerProgress { get; set; } = 5;
    public enum GameStates
    {
        MAINMENU=0,
        INGAME=1,
        GAMEENDED=2
    }
    public static GameStates GameState { get; set; }=GameStates.MAINMENU;
    public override void _Ready()
    {
        EventBus.RegisterCallBack("generate_coins", new Callable(this, nameof(generateCoins)));
    }
    public void generateCoins(Chunk chunk)
    {
       
        var tiles = chunk.getTiles();
        var coinScene = ResourceLoader.Load<PackedScene>("res://entities/scenes/Coin.tscn");
        int coinNumber = new Random().Next(0,12);
        for(int i = 0; i < coinNumber; i++)
        {
            Node3D coin = (Node3D)coinScene.Instantiate();
            int attempts = 100;
            for(int j = 0; j < attempts;j++)
            {
                int x = new Random().Next(0,11);
                int z= new Random().Next(0,chunk.Span);
                int floorTile = tiles[new Vector3I(x, 0, z).ToString()];
                //check if ground below
                if(gridmap.MeshLibrary.GetItemShapes(floorTile).Count>0)
                {

                    if (!tiles.ContainsKey(new Vector3I(x, 1, z).ToString()))
                    {
                        coin.Position = new Vector3(x+0.5f,1,z+0.5f+GeneratedTiles);
                        AddChild(coin);
                        break;
                    }
                }
            }
        }
    }
}
