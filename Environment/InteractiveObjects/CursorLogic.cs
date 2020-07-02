using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLogic : MonoBehaviour
{
    private GameObject currentInteractiveObject, lastInteractiveObject;

    void Start()
    {
        currentInteractiveObject = null;
        lastInteractiveObject = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InteractiveObjectFunction();
    }

    private void InteractiveObjectFunction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (LayerMask.GetMask("InteractiveObject"));
        RaycastHit hitray;
        Vector3 mousePos3d = new Vector3(0f, 0f, 0f);


        lastInteractiveObject = currentInteractiveObject;
        if (Physics.Raycast(ray, out hitray, Mathf.Infinity, layerMask))
        {
            currentInteractiveObject = hitray.transform.gameObject;
        }
        else
        {
            currentInteractiveObject = null;
        }


        if (currentInteractiveObject != lastInteractiveObject)
        {
            if (currentInteractiveObject != null)
            {
                currentInteractiveObject.GetComponent<InteractiveObject>().CursorOverObject();
            }

            if (lastInteractiveObject != null)
            {
                lastInteractiveObject.GetComponent<InteractiveObject>().CursorLeftObject();
            }
        }
    }

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
