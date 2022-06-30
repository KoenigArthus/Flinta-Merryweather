using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    [SerializeField] private ScrPlant plant;
    [SerializeField] private bool didThisOnce;
    private Controller controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
        if (controller.currentSceneWasVisited == false)
        {
            Debug.Log("äaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            plant.conditionsTrue = 0;
        }
    }

    public void AddTrueCondition()
    {
        plant.conditionsTrue++;
    }

    private void FixedUpdate()
    {
        if (plant.conditionsTrue == 2 && !didThisOnce)
        {
            didThisOnce = true;
            gameObject.GetComponent<NoReturn>().AnimateAction();
        }
    }


}
