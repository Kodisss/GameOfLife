using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool alive;

    GameOfLife game;

    private float positionX;
    private float positionY;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOfLife>();
        positionX = transform.position.x;
        positionY = transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckStatus();
        ChangeColor();
    }

    void ChangeColor()
    {
        if (alive)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.black;
        }
    }

    void CheckStatus()
    {
        alive = game.GetStatus((int) positionX, (int) positionY);
    }


}
