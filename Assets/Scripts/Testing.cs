using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    private Grid<Terrain> grid;
    private Pathfinding pathfinding;
    private TileMapVisual TileMapVisual;
    void Start()
    {
        float cellSize = 2f;
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;     
        Vector3 Start = new Vector3(Camera.main.ScreenToWorldPoint(Vector3.zero).x, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0);

        grid = new Grid<Terrain>(Mathf.FloorToInt(width/cellSize), Mathf.FloorToInt(height/cellSize), cellSize, Start, (Grid<Terrain> g, int x, int y) => new Terrain(g, x, y));
        pathfinding = new Pathfinding(grid, Mathf.FloorToInt(width / cellSize), Mathf.FloorToInt(height / cellSize), cellSize, Start);
        TileMapVisual = GetComponent<TileMapVisual>();
        TileMapVisual.SetGrid(grid);
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Terrain ter = grid.GetGridObject(UtilsClass.GetMouseWorldPosition());
            grid.getXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            if (ter != null)
                //ter.SetCost(65);
                pathfinding.FindArea(x, y, 3);

            foreach (PathNode pathNode in pathfinding.FindArea(x, y, 3))
            {
                //Debug.Log(pathNode.GetX() + "," + pathNode.GetY());
            }
        }


        
    }
}

