using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private Grid<PathNode> grid;
    private Grid<Terrain> map;

    public static Pathfinding Instance { get; private set; }

    List<PathNode> openList;
    List<PathNode> closedList;

    public Pathfinding(Grid<Terrain> map, int width, int height, float cellSize, Vector3 originPoition)
    {
        Instance = this;
        this.map = map;
        grid = new Grid<PathNode>(width, height, cellSize, originPoition, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return grid;
    }


    public List<PathNode> FindArea(Vector3 startWorldPosition, int maxCost)
    {
        grid.getXY(startWorldPosition, out int startX, out int startY);
        return FindArea(startX, startY, maxCost);
    }

    public List<PathNode> FindArea(int startX, int startY, int maxCost)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);

        openList = new List<PathNode>() { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.hCost = CalculateDistanceCost(startNode, pathNode);
                pathNode.calculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = 0;
        startNode.calculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            openList.Remove(currentNode);
            closedList.Add(currentNode);
            grid.TriggerGridObjectChanged(currentNode.GetX(), currentNode.GetY());

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode) * map.GetGridObject(neighbourNode.GetX(), neighbourNode.GetY()).GetCost();
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(startNode, neighbourNode);
                    neighbourNode.calculateFCost();

                    if (!openList.Contains(neighbourNode) && neighbourNode.gCost <= maxCost)
                    {                     
                        openList.Add(neighbourNode);
                    }
                }

            }


        }
        return closedList;

    }

    public List<Vector3> FindPath(Vector3 endWorldPoistion)
    {
        grid.getXY(endWorldPoistion, out int endX, out int endY);

        List<PathNode> path = FindPath(endX, endY);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(grid.GetWorldPosition(pathNode.GetX(), pathNode.GetY()) + new Vector3(1, 1, 0) * grid.GetCellSize() * .5f);
            }
            return vectorPath;
        }
    }

    public List<PathNode> FindPath(int endX, int endY)
    {
        PathNode endNode = grid.GetGridObject(endX, endY);

        if(closedList.Contains(endNode))
            return CalculatePath(endNode);
        else
            return null;

    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    { List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.GetX() - 1 >= 0)
            neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY()));
        if (currentNode.GetX() + 1 < grid.GetWidth())
            neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY()));
        if (currentNode.GetY() - 1 >= 0)
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() - 1));
        
        if (currentNode.GetY() + 1 < grid.GetHeight())
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() + 1));
        return neighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;
        path.Add(endNode);
        while (currentNode.cameFromNode != null)
        {
            Debug.Log(currentNode.GetX() + "," + currentNode.GetY());
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                grid.TriggerGridObjectChanged(x, y);
            }
        }

        return path;
    }
    private int CalculateDistanceCost(PathNode startNode, PathNode endNode)
    {
        return (Mathf.Abs(startNode.GetX() - endNode.GetX() + Mathf.Abs(startNode.GetY() - endNode.GetY())));

    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestCostNode.fCost)
                lowestCostNode = pathNodeList[i];
        }
        return lowestCostNode;
    }
}
