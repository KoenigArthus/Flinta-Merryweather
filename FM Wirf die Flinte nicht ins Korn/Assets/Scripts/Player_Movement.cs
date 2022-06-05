using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    //speed of the Player_Character movement
    public AnimationCurve speed;
    //x Boundaries of where the Player_Character can move
    public float MinimumXBoundary, MaximumXBoundary;
    //if the character moves: isMoving = true
    [HideInInspector] public bool isMoving;
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
        /*when the player presses the left mouse button the target position gets updated along its x axis
        isMoving is set to true*/
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
            
            targetPos = new Vector2(pmousePos.x, transform.position.y);

            //Updating The targetPos to be inside the Boundarys if needed
            if (targetPos.x < MinimumXBoundary)
            {
                targetPos.x = MinimumXBoundary;
            }
            else if (targetPos.x > MaximumXBoundary)
            {
                targetPos.x = MaximumXBoundary;
            }

            isMoving = true;
        }
    }

    //Gizmo Drawing for x Boundaries
    private void OnDrawGizmos()
    {
        //the two Boundary Lines
        Gizmos.DrawLine(new Vector3(MinimumXBoundary, -6, 0), new Vector3(MinimumXBoundary, 6, 0));
        Gizmos.DrawLine(new Vector3(MaximumXBoundary, -6, 0), new Vector3(MaximumXBoundary, 6, 0));
        //The HelpLine Laying on the x Axis
        Gizmos.DrawLine(new Vector3(MinimumXBoundary, transform.position.y - 0.5f, 0), new Vector3(MaximumXBoundary, transform.position.y - 0.5f, 0));
    }
}
