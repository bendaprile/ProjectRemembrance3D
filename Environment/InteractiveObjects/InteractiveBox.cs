using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractiveBox : InteractiveThing
{
    [SerializeField] protected Material[] materials;
    [SerializeField] int LockPickingRequirement;
    [SerializeField] public List<GameObject> Items;


    private PlayerStats playerStats;
    private MeshRenderer mesh;


    protected override void Start()
    {
        base.Start();
        mesh = GetComponent<MeshRenderer>();
        UIControl = GameObject.Find("UI").GetComponent<UIController>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public override void CursorOverObject()
    {
        base.CursorOverObject();
        set_item_material(1);
    }

    public override void CursorLeftObject()
    {
        base.CursorLeftObject();
        set_item_material(0);
    }

    protected virtual void set_item_material(int i)
    {
        mesh.material = materials[i];
    }

    // TODO: Handle Input through InputManager and not direct key references
    protected override void ActivateLogic()
    {
        if (playerStats.ReturnNonCombatSkill(SkillsEnum.Larceny) >= LockPickingRequirement)
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
}
