using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain
{
    private int movmentCost;
    private bool walkable;

    private Grid<Terrain> grid;
    private int x;
    private int y;

    public Terrain(Grid<Terrain> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.movmentCost = 1;
    }

    public void SetCost(int cost)
    {
        this.movmentCost = cost;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return "";
        //return movmentCost.ToString();
    }

    public int GetCost()
    {
        return movmentCost;
    }
}
