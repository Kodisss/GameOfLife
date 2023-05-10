using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        while (!game.GetInitialized()) ;
        CheckStatus();
        ChangeColor();
    }

    private void ChangeColor()
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

    private void CheckStatus()
    {
        alive = game.GetStatus((int) positionX, (int) positionY);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        alive = !alive; // Toggle the cell's state on click
    }

    public bool GetAlive()
    {
        return alive;
    }
}
