using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveThing : MonoBehaviour
{
    protected GameObject player;
    protected bool isCursorOverhead;
    protected PlayerInRadius PIR;
    protected GameObject Text;
    protected UIController UIControl;

    protected virtual void Start()
    {
        PIR = GetComponentInChildren<PlayerInRadius>();
        Text = transform.Find("Text").gameObject;
        player = GameObject.Find("Player");
        UIControl = GameObject.Find("UI").GetComponent<UIController>();
    }

    public virtual void CursorOverObject()
    {
        isCursorOverhead = true;
    }

    public virtual void CursorLeftObject()
    {
        isCursorOverhead = false;
    }

    protected virtual void ActivateLogic()
    {

    }


    private void Update()
    {
        if (isCursorOverhead && PIR.isTrue)
        {
            Text.SetActive(true);
            ActivateLogic();
        }
        else
        {
            Text.SetActive(false);
        }
    }
}
