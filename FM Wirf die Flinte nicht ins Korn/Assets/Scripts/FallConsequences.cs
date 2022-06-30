using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallConsequences : MonoBehaviour
{
    private Controller controller;

    private void Start()
    {
        controller = GetComponent<Character>().controller;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //layerAbfrage
        collision.gameObject.SetActive(false);
        StartCoroutine(DespawnBlinking());
    } 

    IEnumerator DespawnBlinking()
    {
        ///Adding the Character to the sceneSave
        //lengthen Scene Save by 1
        string[] lsave = controller.sceneInfo.sceneSave;
        controller.sceneInfo.sceneSave = new string[lsave.Length + 1];
        //restoring Scene Save
        for (int i = 0; i < lsave.Length; i++)
        {
            controller.sceneInfo.sceneSave[i] = lsave[i];
        }
        //adding Item
        for (int j = 0; j < lsave.Length; j++)
        {
            if (controller.sceneInfo.sceneSave[j] == null)
            {
                controller.sceneInfo.sceneSave[j] = this.name;
                break;
            }
        }
        /// End Adding the shootable to the sceneSave
        // Despawn blinking
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
