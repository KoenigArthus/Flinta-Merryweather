using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public GameObject player;
    public float reachRadius = 1f;

    private Vector2 mousePos;
    private RaycastHit2D hit;

    private void Update()
    {
        //Checking if left or right mouse button was clicked & was not over an UI Element
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((Input.GetMouseButtonDown(0) | Input.GetMouseButtonDown(1)) && !EventSystem.current.IsPointerOverGameObject())
        {
            /*if the Mouse hit a collider it tells the GameObject
                            the collider is attached to to call the method "ReactToClick".
                            If no collider was hit the Player_Character will move.
                            */
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && IsInReach() && hit.collider.gameObject.CompareTag("Item"))
            {
                hit.collider.gameObject.SendMessage("ReactToClick", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                player.SendMessage("Move", mousePos, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private bool IsInReach()
    {
        float lhitPosAbsX = System.Math.Abs(hit.collider.gameObject.transform.position.x);
        float lhitPosAbsY = System.Math.Abs(hit.collider.gameObject.transform.position.y);
        float lplayerPosAbsXreach = System.Math.Abs(player.transform.position.x) + reachRadius;
        float lPlayerPosAbsYreach = System.Math.Abs(player.gameObject.transform.position.y) + reachRadius;
        if (lhitPosAbsX < lplayerPosAbsXreach && lhitPosAbsY < lPlayerPosAbsYreach)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
