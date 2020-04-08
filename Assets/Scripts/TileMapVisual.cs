using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TileMapVisual : MonoBehaviour
{
    private Grid<Terrain> grid;
    private Mesh mesh;

    public void SetGrid( Grid<Terrain> grid)
    {
        this.grid = grid;
        UpdateTileMapVisual();
    }

    public void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void UpdateTileMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetHeight() * grid.GetWidth(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;

                Vector3 QuadSize = new Vector3(1, 1) * grid.GetCellSize();
                //Vector3 QuadSize = Vector3.zero;

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + QuadSize * .5f, 0f, QuadSize, Vector2.zero, new Vector2(1,1));

            }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

}
