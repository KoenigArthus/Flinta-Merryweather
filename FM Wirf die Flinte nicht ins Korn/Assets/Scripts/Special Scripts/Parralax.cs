using UnityEngine;

public class Parralax : MonoBehaviour
{
    public float parralaxAmmount;

    private GameObject cam;
    private float startpos;

    //Initializing Position & Camera

    private void Awake()
    {
        startpos = transform.position.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        float distance = cam.transform.position.x * parralaxAmmount;
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    }
    //moving the Object relative to the camera pos by the parralaxAmmount every Fixed Update
    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parralaxAmmount;
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    }
}
