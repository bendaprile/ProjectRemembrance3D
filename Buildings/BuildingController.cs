using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{

    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject lights;
    [SerializeField] private CameraEnvironmentController controller;

    private bool insideBuilding = false;

    private void Start()
    {
        lights.SetActive(false);
        roof.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other is CapsuleCollider)
        {
            roof.SetActive(!roof.activeSelf);
            lights.SetActive(!lights.activeSelf);

            if (insideBuilding == false)
            {
                StartCoroutine(controller.EnterBuilding());
                insideBuilding = true;
            }
            else if (insideBuilding == true)
            {
                StartCoroutine(controller.ExitBuilding());
                insideBuilding = false;
            }
        }
    }
}
