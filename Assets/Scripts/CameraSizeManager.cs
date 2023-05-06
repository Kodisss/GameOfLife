using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    GameOfLife game;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOfLife>();
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize = game.getGridSize() / 2;
    }
}
