using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class Shootable : Interactable
{

    [SerializeField] private ScrShootable target;
    [SerializeField] private GameObject fallItem;

    public Vector2 fallPosition;


    private void Start()
    {
        changesCursorInShotgunState = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = target.sprite;
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
        if (target.despawns && !target.falls)
        {
            StartCoroutine(DespawnBlinking());
        }
        else if (target.falls && !target.despawns)
        {
            StartCoroutine(Falling());
        }

    }


    IEnumerator DespawnBlinking()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
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
        yield return new WaitForSeconds(0.9f);
        for (float i = gameObject.transform.position.y; i > controller.player.transform.position.y; i -= 0.1f)
        {
            gameObject.transform.position += new Vector3(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        fallPosition = this.gameObject.transform.position;
        fallItem.GetComponent<Item>().FallItemSpawn(fallItem, fallPosition);
        this.gameObject.SetActive(false);
        StopCoroutine(Falling());
    }

}
