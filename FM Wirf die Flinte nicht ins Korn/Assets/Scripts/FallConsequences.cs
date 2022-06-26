using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallConsequences : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //layerAbfrage
        collision.gameObject.SetActive(false);
        StartCoroutine(DespawnBlinking());
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

}
