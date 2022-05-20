using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exampleObjektScript : abstractObject
{
    //THIS IS A TEST SCRIPT ihneriting from the abstract class abstractObject
    public override void I()
    {
        Debug.Log("Bro");
    }
    public override void B()
    {
        Debug.Log("Bro");
        base.B();
    }
}
