using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveBox : InteractiveObject
{
    [SerializeField] int LockPickingRequirement;
    [SerializeField] public List<GameObject> Items;

    private PlayerInRadius PIR;
    private GameObject Text;


    private UIController UIControl;
    private GameObject player;
    private PlayerStats playerStats;


    protected override void Start()
    {
        base.Start();
        PIR = GetComponentInChildren<PlayerInRadius>();
        Text = transform.Find("Text").gameObject;
        player = GameObject.Find("Player");
        UIControl = GameObject.Find("UI").GetComponent<UIController>();
        playerStats = player.GetComponent<PlayerStats>();
    }


    private void Update()
    {
        if(isCursorOverhead && PIR.isTrue)
        {
            Text.SetActive(true);
            if (playerStats.ReturnSkill(SkillsEnum.Lockpicking) >= LockPickingRequirement)
            {
                Text.GetComponent<TextMeshPro>().text = "(E) Open";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UIControl.OpenInteractiveMenu(this.gameObject);
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
