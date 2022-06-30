using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shootable : Interactable
{

    [SerializeField] private ScrShootable target;
    [SerializeField] private GameObject fallItem;

    public Vector3 fallPosition;


    private void Start()
    {
        changesCursorInShotgunState = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = target.sprite;
    }

    public override void ReactToClick(Controller pcon)
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeingShot(pcon);
        }
    }

    public void BeingShot(Controller pcon)
    {
 
        if (target.name == "Bierkrug")
        {
            pcon.sceneInfo.tavernenScore += 1;
        }
        if (target.despawns && target.falls)
        {
            Debug.LogError("A Shootable target can only have one true value for despawns or falls");
        }
        else if (target.despawns && !target.falls)
        {
            StartCoroutine(DespawnBlinking(pcon));
        }
        else if (target.falls && !target.despawns)
        {
            StartCoroutine(Falling(pcon));
            
        }
    }

    IEnumerator DespawnBlinking(Controller pcon)
    {
        
        ///Adding the shootable to the sceneSave
        //lengthen Scene Save by 1
        string[] lsave = pcon.sceneInfo.sceneSave;
        pcon.sceneInfo.sceneSave = new string[lsave.Length + 1];
        //restoring Scene Save
        for (int i = 0; i < lsave.Length; i++)
        {
            pcon.sceneInfo.sceneSave[i] = lsave[i];
        }
        //adding Item
        for (int j = 0; j < lsave.Length; j++)
        {
            if (pcon.sceneInfo.sceneSave[j] == null)
            {
                pcon.sceneInfo.sceneSave[j] = this.name;
                break;
            }
        }
        /// End Adding the shootable to the sceneSave

        //Palmenblätter adds to trueConditions in the plant
        if (gameObject.name == "Palmenblätter")
        {
            GameObject lplant = GameObject.Find("Pflanze");
            lplant.GetComponent<PlantGrow>().AddTrueCondition();
        }

        //Blinking Part
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        for (int i = 0; i < 3; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.2f);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }

        this.gameObject.SetActive(false);
        StopCoroutine(DespawnBlinking(pcon));
    }

    IEnumerator Falling(Controller pcon)
    {
        ///Adding the shootable to the sceneSave
        //lengthen Scene Save by 1
        string[] lsave = pcon.sceneInfo.sceneSave;
        pcon.sceneInfo.sceneSave = new string[lsave.Length + 1];
        //restoring Scene Save
        for (int i = 0; i < lsave.Length; i++)
        {
            pcon.sceneInfo.sceneSave[i] = lsave[i];
        }
        //adding Item
        for (int j = 0; j < lsave.Length; j++)
        {
            if (pcon.sceneInfo.sceneSave[j] == null)
            {
                pcon.sceneInfo.sceneSave[j] = name;
                break;
            }
        }
        /// End Adding the shootable to the sceneSave
        Debug.Log("added shootable" + name);
        yield return new WaitForSeconds(0.9f);
        for (float i = gameObject.transform.position.y; i > controller.player.transform.position.y; i -= 0.1f)
        {
            gameObject.transform.position += new Vector3(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        // Instantiating the new Item & seting some variables
        fallPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0.1f);
        Instantiate(fallItem, fallPosition, Quaternion.identity);
        //Adding fallItem to the Instantiate Spawn
        for (int i = 0; i < pcon.sceneInfo.toInstantiateItem.Length; i++)
        {
            if (pcon.sceneInfo.toInstantiateItem[i] == null)
            {
                pcon.sceneInfo.toInstantiateItem[i] = this.fallItem;
                pcon.sceneInfo.itemsSpawnPos[i] = this.fallPosition;
                pcon.sceneInfo.sceneItemLaysIn[i] = SceneManager.GetActiveScene().name;
                break;
            }
        }
        this.gameObject.SetActive(false);
        StopCoroutine(Falling(pcon));
    }
}