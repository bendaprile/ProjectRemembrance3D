using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLogic : MonoBehaviour
{
    private GameObject currentInteractiveThing, lastInteractiveThing;

    void Start()
    {
        currentInteractiveThing = null;
        lastInteractiveThing = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InteractiveObjectFunction();
    }

    // TODO: Handle for controllers
    private void InteractiveObjectFunction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (LayerMask.GetMask("InteractiveThing"));
        RaycastHit hitray;
        Vector3 mousePos3d = new Vector3(0f, 0f, 0f);


        lastInteractiveThing = currentInteractiveThing;
        if (Physics.Raycast(ray, out hitray, Mathf.Infinity, layerMask))
        {
            currentInteractiveThing = hitray.transform.gameObject;
        }
        else
        {
            currentInteractiveThing = null;
        }


        if (currentInteractiveThing != lastInteractiveThing)
        {
            if (currentInteractiveThing != null)
            {
                currentInteractiveThing.GetComponent<InteractiveThing>().CursorOverObject();
            }

            if (lastInteractiveThing != null)
            {
                lastInteractiveThing.GetComponent<InteractiveThing>().CursorLeftObject();
            }
        }
    }

    // TODO: Handle for controllers
    public Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (LayerMask.GetMask("Terrain"));
        RaycastHit hitray;
        Vector3 mousePos3d = new Vector3(0f, 0f, 0f);

        if (Physics.Raycast(ray, out hitray, Mathf.Infinity, layerMask))
        {
            mousePos3d = hitray.point;
        }

        return mousePos3d;
    }
}
