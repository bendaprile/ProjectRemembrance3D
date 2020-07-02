using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveBox : InteractiveObject
{
    [SerializeField] int LockPickingRequirement;
    private PlayerInRadius PIR;
    private GameObject Text;

    private GameObject player;
    private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        PIR = GetComponentInChildren<PlayerInRadius>();
        Text = transform.Find("Text").gameObject;
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }


    private void Update()
    {
        if(isCursorOverhead && PIR.isTrue)
        {
            Text.SetActive(true);
            if (playerStats.ReturnSkill(PlayerStats.SkillsEnum.Lockpicking) >= LockPickingRequirement)
            {
                Text.GetComponent<TextMeshPro>().text = "(E) Open";
                if (Input.GetKeyDown(KeyCode.E))
                {

                }
            }
            else
            {
                Text.GetComponent<TextMeshPro>().text = ("Requirement " + LockPickingRequirement);
            }
        }
        else
        {
            Text.SetActive(false);
        }
    }
}
