using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    private GameOfLife game;
    private Camera mainCamera;

    private float cameraSize;
    private float offset;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeDependencies();
        InitializeAttributes();
        offset = game.CalculateOffset();
        SetupCamera();
    }

    private void InitializeDependencies()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOfLife>();
        mainCamera = GetComponent<Camera>();
    }

    private void InitializeAttributes()
    {
        cameraSize = game.GetGridSize() / 2f;
    }

    private void SetupCamera()
    {
        mainCamera.orthographicSize = cameraSize;
        transform.position = new Vector3(cameraSize + offset / 2, cameraSize + offset / 2, -10f);
    }
}
