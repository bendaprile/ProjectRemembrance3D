using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightController : MonoBehaviour
{

    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject lights;

    private bool insideBuilding = false;

    private void Start()
    {
        if (lights) { lights.SetActive(false); }
        if (roof) { roof.SetActive(true); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other is CapsuleCollider)
        {
            roof.SetActive(!roof.activeSelf);
            lights.SetActive(!lights.activeSelf);

            if (insideBuilding == false)
            {
                insideBuilding = true;
            }
            else if (insideBuilding == true)
            {
                insideBuilding = false;
            }
        }
    }
}
