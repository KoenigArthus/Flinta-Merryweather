using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoReturn : MonoBehaviour
{
   // This class is only here to indicate that no item should be returned when crafting with this.gameObject
   public Animator animator;
   public GameObject dart;
   public Vector3 finalDartPos;

    private void Start()
    {
        if(this.gameObject.GetComponentInParent<Animator>() != null)
        {
            animator = this.gameObject.GetComponentInParent<Animator>();
            this.gameObject.GetComponentInParent<Animator>().enabled = false;
        }
    }


    public void AnimateAction()
    {
        animator.enabled = true;
        Controller lcontroller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
        if (gameObject.name == "Dieter")
        {
            StartCoroutine(DoDartDieter(lcontroller));
        }

        if (gameObject.GetComponent<Item>() != null)
        {

        }

    }

    IEnumerator DoDartDieter(Controller pcon)
    {
        yield return new WaitForSeconds(1f);
        ///Adding the Dart to the sceneSave
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
                pcon.sceneInfo.sceneSave[j] = "Dart";
                break;
            }
        }
        ///End of adding the shootable to the sceneSave
        //Adding dart to the Instantiate Spawn
        for (int i = 0; i < pcon.sceneInfo.toInstantiateItem.Length; i++)
        {
            if (pcon.sceneInfo.toInstantiateItem[i] == null)
            {
                pcon.sceneInfo.toInstantiateItem[i] = this.dart;
                pcon.sceneInfo.itemsSpawnPos[i] = this.finalDartPos;
                pcon.sceneInfo.sceneItemLaysIn[i] = SceneManager.GetActiveScene().name;
                break;
            }
        }
        // starting dialogue
        gameObject.GetComponent<Character>().ReactToClick(pcon, gameObject);
        StopCoroutine(DoDartDieter(pcon));
    }


}
