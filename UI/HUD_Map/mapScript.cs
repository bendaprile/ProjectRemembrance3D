using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    [SerializeField] private float angleoffset = -45f; // x or y size
    [SerializeField] private float mapsize = 0; // x or y size
    [SerializeField] private float Worldmapsize = 0; //One edge
    [SerializeField] private Vector2 TopPoint = new Vector2(); //X then Z

    private float length;
    private Vector2 MapCenter;
    private Transform playerLoc;
    private RectTransform playerIconUI;

    void Start()
    {
        playerLoc = GameObject.Find("Player").transform;
        playerIconUI = transform.Find("PlayerLocation").GetComponent<RectTransform>();

        length = Worldmapsize / 2;

        MapCenter = new Vector2(TopPoint.x - length, TopPoint.y - length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentloc = new Vector2(playerLoc.position.x - MapCenter.x, playerLoc.position.z - MapCenter.y);
        currentloc *= (mapsize / (Worldmapsize));

        Vector2 rotatedloc;
        rotatedloc.x = (currentloc.x - currentloc.y) / 2;
        rotatedloc.y = (currentloc.x + currentloc.y) / 2;

        playerIconUI.localPosition = new Vector3(rotatedloc.x, rotatedloc.y, 0);

        playerIconUI.localEulerAngles = new Vector3(0f, 0f, -(playerLoc.localEulerAngles.y + angleoffset));
    }
}
