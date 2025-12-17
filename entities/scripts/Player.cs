using CrossyRoad.chunks;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody3D
{
    [Export] private MeshInstance3D texture;
    [Export] private GridMap gridMap;
    [Export] private Array<PackedScene> chunksPaths=new Array<PackedScene>();
    private ProbabilityStack probabilityStack;
    private List<Chunk>chunks = new List<Chunk>();
    private int tileX = 5;
    private int tileZ = 0;
    private int score = 0;
    private const float GRAVITY = 0.1f;
    private const float MOVEMENT = 1f;
    private const float BOUNCE = 0.8f;
    private const int inputDeltas = 10;
    private int currentInputDeltas = 0;
    private bool canInput = true;

    private int maxFramesWithoutProgress = 300;
    private int framesWithoutProgress = 0;
    public override void _Ready()
    {
        probabilityStack = new ProbabilityStack();
        foreach (var chunkPath in chunksPaths)
        {
            Chunk chunk = (Chunk)chunkPath.Instantiate();
            chunks.Add(chunk);
            probabilityStack.addChunk(chunk);
        }
        
    }
    public override void _PhysicsProcess(double delta)
    {
        
        if (Game.GameState == Game.GameStates.INGAME)
        {
            framesWithoutProgress++;
            if (framesWithoutProgress == maxFramesWithoutProgress)
            {
                Game.GameState = Game.GameStates.GAMEENDED;
                EventBus.Emit("spawn_replay_button", null);
            }
            bool actionPressed = false;
            float x = 0;
            float y = -GRAVITY;
            float z = 0;
            if (canInput)
            {
                if (Input.IsActionJustPressed("up") && CheckNextPosition(new Vector3I(0, 1, 1)))
                {
                    z += MOVEMENT;
                    y += BOUNCE;
                    tileZ++;
                    Game.PlayerProgress++;
                    if (score < Game.PlayerProgress - 5)
                    {
                        score++;
                        EventBus.Emit("score_adding", 1);
                        framesWithoutProgress = 0;
                    }
                    RotationDegrees = new Vector3(0, -180, 0);
                    if (tileZ > 1) EventBus.Emit("player_moving", new Vector3(0, 0, 1));
                    actionPressed = true;
                }
                if (Input.IsActionJustPressed("down") && tileZ != 0 && CheckNextPosition(new Vector3I(0, 1, -1)))
                {
                    z -= MOVEMENT;
                    y += BOUNCE;
                    tileZ--;
                    Game.PlayerProgress--;
                    RotationDegrees = new Vector3(0, 0, 0);
                    EventBus.Emit("player_moving", new Vector3(0, 0, -1));
                    actionPressed = true;
                }
                if (Input.IsActionJustPressed("left") && tileX != 10 && CheckNextPosition(new Vector3I(1, 1, 0)))
                {
                    x += MOVEMENT;
                    y += BOUNCE;
                    tileX++;
                    RotationDegrees = new Vector3(0, -90, 0);
                    EventBus.Emit("player_moving", new Vector3(1, 0, 0));
                    actionPressed = true;
                }
                if (Input.IsActionJustPressed("right") && tileX != 0 && CheckNextPosition(new Vector3I(-1, 1, 0)))
                {
                    x -= MOVEMENT;
                    y += BOUNCE;
                    tileX--;
                    RotationDegrees = new Vector3(0, 90, 0);
                    EventBus.Emit("player_moving", new Vector3(-1, 0, 0));
                    actionPressed = true;
                }
            }
            Velocity = new Vector3(x, y, z);
            MoveAndCollide(new Vector3(x, y, z));
            if (isOutOfBounds())
            {
                Game.GameState = Game.GameStates.GAMEENDED;
                EventBus.Emit("spawn_replay_button", null);
            }
            if (actionPressed)
            {
                correctPosition();
                TryGenerateRandomChunks();
                canInput = false;
            }
            if (!canInput)
            {
                currentInputDeltas++;
                if (currentInputDeltas == inputDeltas)
                {
                    currentInputDeltas = 0;
                    canInput = true;
                }
            }
        }
    }

    private void correctPosition()
    {
        float x = (float)((int)Position.X + 0.5);
        float z = (float)((int)Position.Z + 0.5);
        Position = new Vector3(x, 1, z);
    }
    public void flatPlayer()
    {
        texture.QueueFree();
        MeshInstance3D meshInstance3D = (MeshInstance3D)ResourceLoader.Load<PackedScene>("res://entities/scenes/FlatPlayer.tscn").Instantiate();
        AddChild(meshInstance3D);
        texture = meshInstance3D;
        Game.GameState = Game.GameStates.GAMEENDED;
        EventBus.Emit("spawn_replay_button", null);
    }
    private bool CheckNextPosition(Vector3I direction)
    {
        var tileID = gridMap.GetCellItem((Vector3I)(Position + direction));
        if (tileID == GridMap.InvalidCellItem) return true;
        else
        {
            var shapes=gridMap.MeshLibrary.GetItemShapes(tileID);
            if (shapes.Count == 0)return true;
            else return false;
        }
    }
    private bool isOutOfBounds()
    {
        if(Position.Y<0) return true;
        if(Position.X<0 || Position.X>11) return true;
        return false;
    }

    private void TryGenerateRandomChunks()
    {
        if (Game.GeneratedTiles - tileZ <= Game.GenerationDistance)
        {
            Chunk choosenChunk = probabilityStack.pickRandomChunk();
            var tiles= choosenChunk.getTiles();
            Vector3I offset = new Vector3I(0, 0, Game.GeneratedTiles);
            foreach (var tile in tiles)
            {
                var str = tile.Key;
                str = str.Replace("(", "").Replace(")", "").Replace(" ","");
                var values = str.Split(",");
                Vector3I position = new Vector3I(Int32.Parse(values[0]), Int32.Parse(values[1]), Int32.Parse(values[2]));
                gridMap.SetCellItem(position+offset,tile.Value);
            }
            Game.GeneratedTiles += choosenChunk.Span;
            var entities=choosenChunk.getEntities();
            foreach (var item in entities)
            {
                Node3D copy=(Node3D)item.Duplicate();
                gridMap.AddChild(copy);
                copy.GlobalPosition += offset;

            }
            EventBus.Emit("generate_coins", choosenChunk);
        }
    }
}
