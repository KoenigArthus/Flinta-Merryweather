using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    private Controller controller;
    private RectTransform rectTransform;
    private Vector3 pos;

    //initializing
    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
        rectTransform = GetComponent<RectTransform>();
    }

    //at the beginning of a drag. The current pos gets saved and controller.isDragging is set to true, to go over to the draggingState
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            controller.isDragging = true;
            pos = rectTransform.anchoredPosition;
        }
    }

    //while dragging with Mouse0 the item stays at the mouse position
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

    //when releasing the drag the item either snaps back to its original pos or gets combined
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            controller.hit = Physics2D.Raycast(controller.mousePos, Vector2.zero);
            if (controller.hit.collider != null)
            {
                Debug.Log(controller.hit.collider.gameObject.name);
                rectTransform.anchoredPosition = pos;
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
