﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeContact : MonoBehaviour
{

    [SerializeField] private float basicAttackForce = 400f;
    [SerializeField] private float heavyAttackForce = 600f;
    [SerializeField] private float basicAttackDamage = 30f;
    [SerializeField] private float heavyAttackDamage = 60f; 

    List<Collider> BoxTriggerList;
    List<Collider> SphereTriggerList;

    LightningMelee lightMelee;

    private float forceMult;
    private float damage;
    private bool AOEsmash;



    private void Start()
    {
        BoxTriggerList = transform.Find("Box").GetComponent<ColliderChild>().TriggerList;
        SphereTriggerList = transform.Find("Sphere").GetComponent<ColliderChild>().TriggerList;
        lightMelee = GameObject.Find("Lightning Melee").GetComponent<LightningMelee>();

        forceMult = 0;
        AOEsmash = false;
    }

    public void MeleeUpdate(int attackAnimIndex)
    {
        if(attackAnimIndex == 2) //TODO CHANGE TO HEAVY
        {
            forceMult = heavyAttackForce;
            damage = heavyAttackDamage;
            AOEsmash = true;
        }
        else
        {
            damage = basicAttackDamage;
            forceMult = basicAttackForce;
            AOEsmash = false;
        }
    }

    public void MeleeContactFunc()
    {
        if (AOEsmash)
        {
            MeleeContactHelper(SphereTriggerList);
        }
        else
        {
            MeleeContactHelper(BoxTriggerList);
        }
    }

    public void MeleeContactHelper(List<Collider> TriggerList)
    {
        int potential_enemies_in_range = TriggerList.Count; //Includes recently dead enemies
        for (int i = potential_enemies_in_range - 1; i >= 0; i--)
        {
            Collider col = TriggerList[i];
            if (!col || col.tag != "BasicEnemy")
            {
                TriggerList.Remove(col);
            }
            else
            {
                Vector3 directionToTarget = col.transform.position - transform.position;
                directionToTarget.y = 0;
                directionToTarget.Normalize();
                Vector3 force = new Vector3(forceMult * directionToTarget.x, 0, forceMult * directionToTarget.z);
                lightMelee.MeleeAttack(col);
                col.GetComponent<Health>().take_damage(damage, true, force);
            }
        }
    }
}
