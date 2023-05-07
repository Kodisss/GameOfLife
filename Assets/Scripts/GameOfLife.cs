using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOfLife : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab of the tile object
    [SerializeField] private int gridSize = 20; // Size of the grid
    [SerializeField] private string mode = "endless"; // Borders gestion "endless" or "mirror"
    [SerializeField] private float delayDuration = 1f;

    private float delayTimer = 0f;

    private bool[,] cellGrid;
    private bool[,] nextGeneration;
    private int offset;
    private int sizeWithOffset;

    [SerializeField] private bool debug = false;

    // Start is called before the first frame update
    private void Start()
    {
        offset = CalculateOffset();
        sizeWithOffset = gridSize + offset;
        CreateGrids();
        SpawnTiles();
    }

    // Update is called once per frame
    private void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer >= delayDuration)
        {
            delayTimer = 0f;
            if(debug) Debug.Log("Delay complete");
            UpdateGrid();
        }
    }

    /////////////////// GETTERS AND SETTERS /////////////////////
    public int GetGridSize()
    {
        return gridSize;
    }

    public bool GetStatus(int x, int y)
    {
        return cellGrid[x, y];
    }

    public string GetMode()
    {
        return mode;
    }
    //////////////////////////////////////////////////////////////

    private void UpdateGrid()
    {
        bool[,] newGeneration = new bool[sizeWithOffset, sizeWithOffset]; // Create a new array for the next generation

        for (int i = 0; i < sizeWithOffset; i++)
        {
            for (int j = 0; j < sizeWithOffset; j++)
            {
                LifeOrDeathOfACell(i, j, CheckNeighboorCells(i, j));
                newGeneration[i, j] = nextGeneration[i, j]; // Copy the updated state to the new array
            }
        }

        cellGrid = newGeneration; // Assign the new generation to cellGrid
    }

    private int CheckNeighboorCells(int positionX, int positionY)
    {
        int res = 0;

        for(int i = positionX - 1; i <= positionX + 1; i++)
        {
            for(int j = positionY - 1; j <= positionY + 1; j++)
            {
                res += BorderGestion(positionX, positionY, i, j);
            }
        }

        if(debug && res > 1 && res < 6) Debug.Log("Cell[" + positionX + "][" + positionY + "] has " + res + " neighboors.");

        return res;
    }

    private int BorderGestion(int cellPositionX, int cellPositionY, int checkedPositionX, int checkedPositionY)
    {
        if(mode == "endless")
        {
            if (checkedPositionX < 1 || checkedPositionY < 1 || checkedPositionX >= sizeWithOffset - 1 || checkedPositionY >= sizeWithOffset - 1)
            {
                return 10;
            }
            else if (checkedPositionX == cellPositionX && checkedPositionY == cellPositionY)
            {
                return 0;
            }
            else if(cellGrid[checkedPositionX, checkedPositionY])
            {
                //if (debug) Debug.Log("i = " + i + " j = " + j);
                return 1;
            }
        }
        else if(mode == "mirror")
        {
            checkedPositionX = (checkedPositionX + sizeWithOffset) % sizeWithOffset;
            checkedPositionY = (checkedPositionY + sizeWithOffset) % sizeWithOffset;

            if (checkedPositionX == cellPositionX && checkedPositionY == cellPositionY)
            {
                return 0;
            }
            else if (cellGrid[checkedPositionX, checkedPositionY])
            {
                //if (debug) Debug.Log("i = " + i + " j = " + j);
                return 1;
            }
        }

        return 0;
    }

    private void LifeOrDeathOfACell(int positionX, int positionY, int neighboorsValue)
    {
        if (neighboorsValue == 3)
        {
            nextGeneration[positionX, positionY] = true;
        }
        else if (neighboorsValue == 2)
        {
            nextGeneration[positionX, positionY] = cellGrid[positionX, positionY];
        }
        else 
        {
            nextGeneration[positionX, positionY] = false;
        }
    }

    private void SpawnTiles()
    {
        // Iterate through the grid
        for (int x = 0; x < sizeWithOffset; x++)
        {
            for (int y = 0; y < sizeWithOffset; y++)
            {
                // Calculate the position of the tile
                Vector3 CellPosition = new Vector3(x, y, 0);

                // Spawn a Cell at the calculated position
                Instantiate(tilePrefab, CellPosition, Quaternion.identity);
            }
        }
    }

    private void CreateGrids()
    {
        cellGrid = new bool[sizeWithOffset, sizeWithOffset];
        nextGeneration = new bool[sizeWithOffset, sizeWithOffset];

        for (int i = 0; i < sizeWithOffset; i++)
        {
            for (int j = 0; j < sizeWithOffset; j++)
            {
                cellGrid[i, j] = Random.value < 0.5f;
                nextGeneration[i, j] = false;
            }
        }
    }

    public int CalculateOffset()
    {
        if (mode == "endless")
        {
            return 12;
        }
        else if (mode == "mirror")
        {
            return 0;
        }

        return 0;
    }

    /////////////////// STRUCTURE MAKERS /////////////////////
    public void ButtonSpawnGlider()
    {
        cellGrid[8, 18] = true;
        cellGrid[9, 17] = true;
        cellGrid[10, 17] = true;
        cellGrid[10, 18] = true;
        cellGrid[10, 19] = true;
    }

    public void SpawnGlider(int posX = 6, int posY = 17)
    {
        cellGrid[posX - 2, posY + 1] = true;
        cellGrid[posX - 1, posY] = true;
        cellGrid[posX, posY] = true;
        cellGrid[posX, posY + 1] = true;
        cellGrid[posX, posY + 2] = true;
    }
    /////////////////////////////////////////////////////////////
}
