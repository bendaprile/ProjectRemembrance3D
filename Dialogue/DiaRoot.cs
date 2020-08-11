using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaRoot : MonoBehaviour
{
    [SerializeField] private Transform StartingTrans;

    public void ModifyStarting(Transform newStart)
    {
        StartingTrans = newStart;
    }

    public Transform ReturnStarting()
    {
        return StartingTrans;
    }

}
