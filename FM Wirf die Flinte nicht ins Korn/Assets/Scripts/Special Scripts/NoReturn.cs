using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReturn : MonoBehaviour
{
   // This class is only here to indicate that no item should be returned when crafting with this.gameObject
   public Animator animator;

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
        if (gameObject.GetComponent<Character>() != null)
        {
            Controller lcontroller = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Controller>();
            //gameObject.GetComponent<Character>().ReactToClick(lcontroller, gameObject);
        }
    }


}
