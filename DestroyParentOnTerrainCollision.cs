using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnTerrainCollision : MonoBehaviour
{
    [SerializeField] GameObject parent = null;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            Destroy(parent);
        }
    }
}
