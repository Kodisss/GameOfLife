using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab of the tile object
    [SerializeField] private int gridSize = 20; // Size of the grid

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getGridSize()
    {
        return gridSize;
    }
}
