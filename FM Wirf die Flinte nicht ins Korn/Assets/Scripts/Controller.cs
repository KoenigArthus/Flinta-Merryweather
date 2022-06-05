using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public float reachRadius = 2f;

    [HideInInspector] public bool isTalking;

    private SmalDialogueManager smalDialogueManager;
    private GameObject player;
    private Vector2 mousePos;
    private RaycastHit2D hit;

    //Intitializing the Player
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        smalDialogueManager = this.GameObject().GetComponent<SmalDialogueManager>();
    }

    //Analysing the players actions
    private void Update()
    {
        //Checking if left or right mouse button was clicked & was not over an UI Element
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1) | Input.GetMouseButtonDown(2)) && !EventSystem.current.IsPointerOverGameObject())
        {
            /*if the Mouse hit a collider it tells the GameObject
              the collider is attached to to call the method "ReactToClick".
              If no collider was hit the Player_Character will move. */
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (!isTalking & hit.collider != null && IsInReach() && hit.collider.gameObject.CompareTag("Interactable"))
            {
                hit.collider.gameObject.SendMessage("ReactToClick", SendMessageOptions.DontRequireReceiver);
            }/*For Debugging Only
            else if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }*/
            else if (isTalking)
            {
                smalDialogueManager.DisplayNextSentence();
            }
            else
            {
                player.SendMessage("Move", mousePos, SendMessageOptions.DontRequireReceiver);
            }


        }

    }

    // Debug Method to See the reachRadius
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").transform.position, reachRadius);
    }



    //Checking if the Interactable of hit is in reach of the player
    private bool IsInReach()
    {
        // calculating the distance of Player to Item: P(x1,y1), I(y2,y1),  distance = √((y2-y1)^2 + (x2-x1)^2)
        // and seeing if the distance is smaller than the defined radius
        return Mathf.Sqrt(
               Mathf.Pow(hit.collider.gameObject.transform.position.y - player.transform.position.y, 2)
             + Mathf.Pow(hit.collider.gameObject.transform.position.x - player.transform.position.x, 2))
            <= reachRadius;

    }
}
