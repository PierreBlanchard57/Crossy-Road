using Godot;
using Godot.Collections;
using System;

public partial class Chunk : GridMap
{
    [Export]public int Span {  get; set; }
    [Export]public int Weight { get; set; }
    public Dictionary<string,int> getTiles()
    {
        var usedCells=GetUsedCells();
        Dictionary<string, int> tiles=new Dictionary<string, int>();
        foreach (var cell in usedCells)
        {
            tiles.Add(cell.ToString(),GetCellItem(cell));
        }
            return tiles;
    }
    public Array<Node3D> getEntities()
    {
        Array<Node3D> res=new Array<Node3D>();
        foreach(var child in GetChildren())
        {
            res.Add((Node3D)child);
        }
        return res;
    }
}
