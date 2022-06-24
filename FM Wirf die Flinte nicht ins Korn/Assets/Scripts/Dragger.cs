using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, /*IPointerDownHandler,*/ IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    private Controller controller;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 pos;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
    }


    /*public void OnPointerDown(PointerEventData eventData)
    {
        // pos = rectTransform.anchoredPosition;
        //Cursor.SetCursor(controller.cursor1, controller.cursorHotspot, CursorMode.ForceSoftware);
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            Debug.Log("Begin Drag");
            controller.isDragging = true;
            pos = rectTransform.anchoredPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            if (controller.currentGameState == controller.draggingState)
            {
                transform.position = Input.mousePosition;
            }
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            Debug.Log("End Drag");
            controller.hit = Physics2D.Raycast(controller.mousePos, Vector2.zero);
            if (controller.hit.collider != null)
            {
                Debug.Log(controller.hit.collider.gameObject.name);
            }
            else
            {
                rectTransform.anchoredPosition = pos;
            }

            controller.isDragging = false;
        }
    }

    //deactivates Drag Theshold (the drag reacts now imediatly)
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}
