using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    //speed of the maincharacter movement
    public AnimationCurve speed;
    //if the character moves: isMoving = true
    [SerializeField] private bool isMoving;
    //goal position after moving
    private Vector2 targetPos;


    //setting up the current position of the player_Character so that it wont skip to 0,0 when later called in the Update function
    private void Start()
    {
        targetPos = new Vector2(transform.position.x, transform.position.y);
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
            //checking if the player_character reachet targetPos and if true sets isMoving to false
            if (transform.position.x == targetPos.x)
            {
                isMoving = false;
            }
        }
    }

    public void Move(Vector2 pmousePos)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // flipping the player_character if needed
            if (pmousePos.x < transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            //when the player presses the left mouse button the target position gets updated along its x axis
            //isMoving is set to true
            targetPos = new Vector2(pmousePos.x, transform.position.y);
            isMoving = true;
        }
    }
}
