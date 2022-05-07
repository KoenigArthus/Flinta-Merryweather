using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //speed of the maincharacter movement
    public AnimationCurve speed;
    //goal position after moving
    private Vector2 targetPos;
    //if the character moves: isMoving = true
    public bool isMoving;
    //setting up the current position of the player_Character so that it wont skip to 0,0 when later called in the Update function
    private void Start()
    {
        targetPos = new Vector2(transform.position.x, transform.position.y);
    }


    private void Update()
    {
        // checking if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //determining the mouse position
            Vector2 lmousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //when the player presses the left mouse button the target position gets updated along its x axis
            targetPos = new Vector2(lmousePos.x, transform.position.y);
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        //checking if the player_Character is Moving
        if (isMoving)
        {
            // the player_character adjusts its position to the targetPos and moves to it with the speed relative to the distance of targetPos
            float ldistanceToTargetPos = Vector2.Distance(transform.position, targetPos);
            float lstep = this.speed.Evaluate(1 / ldistanceToTargetPos) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, lstep);
            if (transform.position.x == targetPos.x && transform.position.y == targetPos.y)
            {
                isMoving = false;
            }
        }
    }
}
