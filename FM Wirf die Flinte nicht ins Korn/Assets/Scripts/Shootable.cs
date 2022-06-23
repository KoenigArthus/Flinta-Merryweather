using System.Collections;
using UnityEngine;

public class Shootable : Interactable
{

    [SerializeField] private ScrShootable target;
    [SerializeField] private GameObject fallItem;


    private void Start()
    {
        changesCursorInShotgunState = true;
    }

    public override void ReactToClick(Controller pcon)
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeingShot();
        }
    }

    public void BeingShot()
    {

        if (target.despawns)
        {

            StartCoroutine(DespawnBlinking());

        }
        else if (target.falls)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //this.gameObject.transform.position = new Vector3(0, 0, 0);
            fallItem.SetActive(true);
            StartCoroutine(Falling());
            
        }







    }


    IEnumerator DespawnBlinking()
    {
        for (int i = 0; i < 3; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            yield return new WaitForSeconds(0.2f);

            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

            yield return new WaitForSeconds(0.2f);
        }
        this.gameObject.SetActive(false);
        StopCoroutine(DespawnBlinking());
    }

    IEnumerator Falling()
    {

        for (float i = fallItem.transform.position.y; i > controller.player.transform.position.y; i -= 0.1f)
        {
            fallItem.transform.position += new Vector3(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
       
        this.gameObject.SetActive(false);

        StopCoroutine(Falling());
    }
}
