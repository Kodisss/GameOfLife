using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab of the tile object
    [SerializeField] private int gridSize = 20; // Size of the grid

    // Start is called before the first frame update
    private void Start()
    {
        SpawnTiles();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SpawnTiles()
    {
        // Iterate through the grid
        for (int x = -gridSize/2; x < gridSize/2; x++)
        {
            for (int y = -gridSize/2; y < gridSize/2; y++)
            {
                // Calculate the position of the tile
                Vector3 tilePosition = new Vector3(x, y, 0);

                // Spawn a tile at the calculated position
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            }
        }
    }

    public int getGridSize()
    {
        return gridSize;
    }
}
