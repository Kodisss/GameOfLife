using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab of the tile object

    private GameOfLife game;
    private int gridSize;

    private void Start()
    {
        InitializeDependencies();
        InitializeAttributes();
        CreateGrid();
    }

    private void InitializeDependencies()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOfLife>();
    }

    private void InitializeAttributes()
    {
        gridSize = game.GetGridSize() + game.CalculateOffset();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 cellPosition = new Vector3(x, y, 0);
                GameObject cell = Instantiate(tilePrefab, cellPosition, Quaternion.identity, transform);
            }
        }
    }
}