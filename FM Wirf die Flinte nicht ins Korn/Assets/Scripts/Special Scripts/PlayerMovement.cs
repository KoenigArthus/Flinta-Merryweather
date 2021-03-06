using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //speed of the Player_Character movement
    public AnimationCurve speed;
    //x Boundaries of where the Player_Character can move
    public float MinimumXBoundary, MaximumXBoundary;
    //if the character moves: isMoving = true
    [HideInInspector] public bool isMoving;
    [HideInInspector] Controller controller;
    //goal position after moving
    private Vector3 targetPos;
    private Animator animator;


    //setting up the current position of the player_Character so that it wont skip to 0,0 when later called in the Update function
    private void Awake()
    {
        controller = controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
        targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //checking if the player_Character is Moving
        if (isMoving)
        {
            // the player_character adjusts its position to the targetPos and moves to it with the speed relative to the distance of targetPos
            float ldistanceToTargetPos = Vector2.Distance(transform.position, targetPos);
            float lstep = this.speed.Evaluate(1 / ldistanceToTargetPos) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, lstep);
            //checking if the player_character reachet targetPos and if true sets isMoving to false
            if (transform.position.x == targetPos.x)
            {
                this.Stop();
            }
        }
    }

   
    // The Player_Character will move the given Vector2 Position
    public void MoveTo(Vector2 pmousePos)
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
            
            targetPos = new Vector3(pmousePos.x, transform.position.y, transform.position.z);

            //Updating The targetPos to be inside the Boundarys if needed
            if (targetPos.x < MinimumXBoundary)
            {
                targetPos.x = MinimumXBoundary;
            }
            else if (targetPos.x > MaximumXBoundary)
            {
                targetPos.x = MaximumXBoundary;
            }

            //Audio
            if (!isMoving)
            {

                if (controller.currentScene == "Piratenstadt")
                {
                    controller.audioManager.Play("Walk on Cobblestone");
                }
                else if (controller.currentScene == "Strand")
                {
                    controller.audioManager.Play("Walk on Sand");
                }
                else if (controller.currentScene == "H?hle")
                {
                    controller.audioManager.Play("Walk on Stone");
                }
                else if (controller.currentScene == "Taverne")
                {
                    controller.audioManager.Play("Walk on Wood");
                }
            }

            isMoving = true;
            animator.SetBool("isMoving", isMoving);

        }
    }

    public void Stop()
    {
        isMoving = false;
        animator.SetBool("isMoving", isMoving);

        //Audio
        if (controller.currentScene == "Piratenstadt")
        {
            controller.audioManager.Stop("Walk on Cobblestone");
        }
        else if (controller.currentScene == "Strand")
        {
            controller.audioManager.Stop("Walk on Sand");
        }
        else if (controller.currentScene == "H?hle")
        {
            controller.audioManager.Stop("Walk on Stone");
        }
        else if (controller.currentScene == "Taverne")
        {
            controller.audioManager.Stop("Walk on Wood");
        }
    }




    //Gizmo Drawing for x Boundaries
    private void OnDrawGizmos()
    {
        //the two Boundary Lines
        Gizmos.DrawLine(new Vector3(MinimumXBoundary, -6, transform.position.z), new Vector3(MinimumXBoundary, 6, transform.position.z));
        Gizmos.DrawLine(new Vector3(MaximumXBoundary, -6, transform.position.z), new Vector3(MaximumXBoundary, 6, transform.position.z));
        //The HelpLine Laying on the x Axis
        Gizmos.DrawLine(new Vector3(MinimumXBoundary, transform.position.y - 0.5f, transform.position.z), new Vector3(MaximumXBoundary, transform.position.y - 0.5f, transform.position.z));
    }
}
