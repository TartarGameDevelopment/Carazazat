using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private int x;
    private int y;
    private Grid<PathNode> grid;
    private Grid<Terrain> map;

    public int gCost;
    public int fCost;
    public int hCost;

    public PathNode cameFromNode;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        //this.map = map;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public override string ToString()
    {
        return gCost.ToString();
    }

    public void calculateFCost()
    {
        fCost = gCost + hCost;
    }

}
