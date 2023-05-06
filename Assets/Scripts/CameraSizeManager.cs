using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    private GameOfLife game;
    private Camera mainCamera;
    private float cameraSize;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOfLife>();
        mainCamera = GetComponent<Camera>();
        cameraSize = game.GetGridSize() / 2f;
        mainCamera.orthographicSize = cameraSize;
        transform.position = new Vector3(cameraSize, cameraSize, -10f);
    }
}
