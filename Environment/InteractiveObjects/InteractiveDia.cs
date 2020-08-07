using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDia : InteractiveThing
{
    [SerializeField] private Transform DiaTrans;
    [SerializeField] private string StartingPoint;

    [SerializeField] private NPC npc;


    public void UpdateStartingPoint(string input)
    {
        StartingPoint = input;
    }

    protected override void ActivateLogic()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UIControl.DialogueMenuBool(DiaTrans, StartingPoint);

            if(npc != null)
            {
                npc.DiaStarted();
            }
        }
    }
}
