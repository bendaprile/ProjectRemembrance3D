using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING_DIA_START : MonoBehaviour
{
    private UIController uI;

    bool LateStart;

    void Start()
    {
        LateStart = true;

    }

    private void FixedUpdate()
    {
        if (LateStart)
        {
            LateStart = false;
            uI = GameObject.Find("UI").GetComponent<UIController>();
            uI.DialogueMenuBool(transform, "Starting");
        }
    }
}
