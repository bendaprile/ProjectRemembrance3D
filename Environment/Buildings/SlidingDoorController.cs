using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    public enum OpenDirections
    {
        Up,
        Down,
        Right,
        Left
    }

    [SerializeField] float doorSize = 2.5f;
    [SerializeField] float openSpeed = 2.0f;
    [SerializeField] OpenDirections openDirection = OpenDirections.Down;
    [SerializeField] bool automaticDoor = true;
 
    private Transform doorTransform;
    private Vector3 defaultDoorPosition;
    private bool open = false;
    private bool doorClosed = true;

    private void Start()
    {
        doorTransform = GetComponentInChildren<MeshRenderer>().gameObject.transform;

        if (doorTransform)
        {
            defaultDoorPosition = doorTransform.transform.localPosition;
        }
    }

    private void Update()
    {
        if (!doorTransform || doorClosed)
        {
            return;
        }

        if (Mathf.Abs(doorTransform.localPosition.x) < 0.001
            && Mathf.Abs(doorTransform.localPosition.y) < 0.001
            && open == false)
        {
            doorClosed = true;
            doorTransform.localPosition = new Vector3(0, 0, 0);
            return;
        }

        switch (openDirection)
        {
            case OpenDirections.Up:
                doorTransform.localPosition = new Vector3(doorTransform.localPosition.x, Mathf.Lerp(
                    doorTransform.localPosition.y, defaultDoorPosition.y + (open ? doorSize : 0), Time.deltaTime * openSpeed), 
                    doorTransform.localPosition.z);
                break;
            case OpenDirections.Down:
                doorTransform.localPosition = new Vector3(doorTransform.localPosition.x, Mathf.Lerp(
                    doorTransform.localPosition.y, -(defaultDoorPosition.y + (open ? doorSize : 0)), Time.deltaTime * openSpeed),
                    doorTransform.localPosition.z);
                break;
            case OpenDirections.Right:
                doorTransform.localPosition = new Vector3(Mathf.Lerp(
                    doorTransform.localPosition.x, defaultDoorPosition.x + (open ? doorSize : 0), Time.deltaTime * openSpeed), 
                        doorTransform.localPosition.y, 
                        doorTransform.localPosition.z);
                break;
            case OpenDirections.Left:
                doorTransform.localPosition = new Vector3(Mathf.Lerp(
                    doorTransform.localPosition.x, -(defaultDoorPosition.x + (open ? doorSize : 0)), Time.deltaTime * openSpeed),
                        doorTransform.localPosition.y,
                        doorTransform.localPosition.z);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!automaticDoor) { return; }

        if (other.tag == "Player")
        {
            open = true;
            doorClosed = false;
        }
    }

    // TODO: Handle Input through InputManager and not direct key references
    private void OnTriggerStay(Collider other)
    {
        if (automaticDoor) { return; }

        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                open = true;
                doorClosed = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            open = false;
        }
    }
}
