using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private Vector2 mousePos;
    private RaycastHit2D hit;


    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.gameObject.SendMessage("SayHello");
            }
            else
            {
                Debug.Log("nothing was hit");
            }
        }
    }

    





}
