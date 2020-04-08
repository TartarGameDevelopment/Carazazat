using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private int x;
    private int y;
    private Grid<PathNode> grid;
    private Grid<Terrain> map;
    private GameObject player;

    public int gCost;
    public int fCost;
    public int hCost;
    public bool walkable;

    public PathNode cameFromNode;
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        //this.map = map;
        this.walkable = true;
    }

    public void SetPlayer( GameObject player)
    {
        this.player = player;
        if (player != null)
        {
            walkable = false;
        }
        else
        {
            walkable = true;
        }
    }

    public GameObject GetPlayer()
    {
        return player;
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
