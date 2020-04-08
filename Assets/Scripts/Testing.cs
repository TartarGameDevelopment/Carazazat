using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    public GameObject Grid;

    private Grid<Terrain> grid;
    private Pathfinding pathfinding;
    private TileMapVisual TileMapVisual;
    void Start()
    {
        TileMapVisual = Grid.GetComponent<TileMapVisual>();
        float cellSize = 2f;
        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;     
        Vector3 Start = new Vector3(Camera.main.ScreenToWorldPoint(Vector3.zero).x, Camera.main.ScreenToWorldPoint(Vector3.zero).y, 0);

        grid = new Grid<Terrain>(Mathf.FloorToInt(width/cellSize), Mathf.FloorToInt(height/cellSize), cellSize, Start, (Grid<Terrain> g, int x, int y) => new Terrain(g, x, y));
        pathfinding = new Pathfinding(grid, Mathf.FloorToInt(width / cellSize), Mathf.FloorToInt(height / cellSize), cellSize, Start);
        TileMapVisual.SetGrid(grid);
    }

    // Update is called once per frame

    void Update()
    {
        


        
    }
}

