using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //speed of the maincharacter movement
    public float speed = 10f;
    //goal position after moving
    private Vector2 targetPos;

    //setting up the current position of the player_Character so that it wont skip to 0,0 when later called in the Update function
    private void Start()
    {
        targetPos = new Vector2(transform.position.x, transform.position.y);
    }
    private void Update()
    {
        //determining the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //when the player presses the left mouse button the target position gets updated along its x axis
        if (Input.GetMouseButtonDown(0))
        {
            targetPos = new Vector2(mousePos.x, transform.position.y);
        }
        // the player_character adjusts its position to the targetPos and moves to it with the set speed
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
    }
}
