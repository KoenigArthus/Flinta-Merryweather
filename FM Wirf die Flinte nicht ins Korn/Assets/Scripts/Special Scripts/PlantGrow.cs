using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PlantGrow : MonoBehaviour
{
    [SerializeField ]private ScrPlant plant;
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
        if (plant.conditionsTrue == 2)
        {
            gameObject.GetComponent<NoReturn>().AnimateAction();
        }
    }


}
