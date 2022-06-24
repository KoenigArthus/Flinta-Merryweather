using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
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


    public void OnPointerDown(PointerEventData eventData)
    {
        pos = rectTransform.anchoredPosition;
        //Cursor.SetCursor(controller.cursor1, controller.cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //pos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(controller.currentGameState == controller.exploreState)
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = pos;
        //Cursor.SetCursor(controller.cursor0, controller.cursorHotspot, CursorMode.ForceSoftware);
    }


    //deactivates Drag Theshold (the drag reacts now imediatly)
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}
