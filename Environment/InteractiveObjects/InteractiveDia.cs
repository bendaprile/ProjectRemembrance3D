using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDia : InteractiveThing
{
    [SerializeField] private Transform DiaTrans;

    protected override void ActivateLogic()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIControl.DialogueMenuBool(DiaTrans);
        }
    }
}
