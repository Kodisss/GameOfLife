using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab of the tile object
    [SerializeField] private int gridSize = 20; // Size of the grid

    private bool[,] cellGrid;

    // Start is called before the first frame update
    private void Start()
    {
        CreateGrid();
        SpawnTiles();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SpawnTiles()
    {
        // Iterate through the grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Calculate the position of the tile
                Vector3 tilePosition = new Vector3(x, y, 0);

                // Spawn a tile at the calculated position
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            }
        }
    }

    private void CreateGrid()
    {
        cellGrid = new bool[gridSize, gridSize];
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public bool GetStatus(int x, int y)
    {
        return cellGrid[x, y];
    }
}
