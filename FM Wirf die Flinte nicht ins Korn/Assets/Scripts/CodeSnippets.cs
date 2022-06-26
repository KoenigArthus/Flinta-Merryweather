
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CodeSnippets
{
    /*<Summary>
     * This class is only used for saving some code snippets from time to time
     * aka. saving the time for a google search because u forgot something u googled yesterday
     * this class could have been a text file but ya know the lazy programmer is the lazy programmer
     * <Summary>
     */


    /*foreach(var sentence in sentences)
      {
         Debug.Log(sentence);
      }*/

}

public class MyVirtualCursor : MonoBehaviour
{

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;


    void Update()
    {

        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the game object
        m_PointerEventData.position = this.transform.localPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0) Debug.Log("Hit " + results[0].gameObject.name);

    }
}


