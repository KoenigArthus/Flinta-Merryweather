using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float parralaxAmmount;

    private GameObject cam;
    private float length, startpos;

    private void Start()
    {
        startpos = transform.position.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parralaxAmmount;
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    }
}
