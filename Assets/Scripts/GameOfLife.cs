using System.Collections;
using System.Collections.Generic;
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
        CalculateOffset();
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
        for(int i = 0; i < sizeWithOffset; i++)
        {
            for(int j = 0; j < sizeWithOffset; j++)
            {
                LifeOrDeathOfACell(i, j, CheckNeighboorCells(i, j));
            }
        }
        cellGrid = nextGeneration;
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
        if(debug) Debug.Log("Cell[" + positionX + "][" + positionY + "] has " + res + " neighboors.");

        return res;
    }

    private int BorderGestion(int cellPositionX, int cellPositionY, int checkedPositionX, int checkedPositionY)
    {
        if(mode == "endless")
        {
            if (checkedPositionX < 0 || checkedPositionY < 0 || checkedPositionX >= sizeWithOffset || checkedPositionY >= sizeWithOffset) { }
            else if (checkedPositionX == cellPositionX && checkedPositionY == cellPositionY) { }
            else
            {
                //if (debug) Debug.Log("i = " + i + " j = " + j);
                if (cellGrid[checkedPositionX, checkedPositionY]) return 1;
            }
        }
        else if(mode == "mirror")
        {
            if (checkedPositionX < 0)
            {
                checkedPositionX = sizeWithOffset;
            }
            if (checkedPositionY < 0)
            {
                checkedPositionY = sizeWithOffset;
            }
            if (checkedPositionX >= sizeWithOffset)
            {
                checkedPositionX = 0;
            }
            if (checkedPositionY >= sizeWithOffset)
            {
                checkedPositionY = 0;
            }
            if (checkedPositionX == cellPositionX && checkedPositionY == cellPositionY) { }
            else
            {
                //if (debug) Debug.Log("i = " + i + " j = " + j);
                if (cellGrid[checkedPositionX, checkedPositionY]) return 1;
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
            nextGeneration[positionY, positionX] = cellGrid[positionX, positionY];
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
                cellGrid[i, j] = false;
                nextGeneration[i, j] = false;
            }
        }
        cellGrid[4, 4] = true;
        cellGrid[3, 4] = true;
        cellGrid[4, 3] = true;
        cellGrid[3, 3] = true;
    }

    private void CalculateOffset()
    {
        if (mode == "endless")
        {
            offset = 6;
        }
        else if (mode == "mirror")
        {
            offset = 0;
        }
    }
}
