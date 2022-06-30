using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    private Controller controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
    }

    public void AddTrueCondition()
    {
        Debug.Log("imma ad one");
        controller.sceneInfo.plantScore++;
    }

    private void FixedUpdate()
    {
        if (controller.sceneInfo.plantScore == 2 && controller.sceneInfo.plantHasGrown == false)
        {
            controller.sceneInfo.plantHasGrown = true;
            gameObject.GetComponent<NoReturn>().AnimateAction();
        }
    }


}
