﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlastCast : Ability
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float damage = 1f;

    private GameObject Lightning;
    private Transform LightningEnd;

    public bool activated;

    void Start()
    {
        Lightning = transform.Find("SmallLightning").gameObject;
        LightningEnd = Lightning.transform.Find("LightningEnd");
        energy = GameObject.Find("Player").GetComponent<Energy>();

        Lightning.SetActive(false);
        activated = false;
    }

    public override void AttemptAttack() //Override this one because the energy is not used up front
    {
        activated = !activated;
        Lightning.SetActive(activated);
    }

    void FixedUpdate()
    {
        if(activated)
        {
            if (!energy.drain_energy(energyCost))
            {
                activated = !activated;
                Lightning.SetActive(activated);
            }

            RaycastHit hitray;
            int layerMask = (LayerMask.GetMask("Player") | LayerMask.GetMask("Ignore Raycast"));
            layerMask = ~layerMask;

            LightningEnd.localPosition = new Vector3(0, 0, 10);

            if (Physics.Raycast(transform.position, transform.forward, out hitray, range, layerMask))
            {
                LightningEnd.position = hitray.collider.transform.position;
                if (hitray.collider.tag == "BasicEnemy")
                {
                    (hitray.collider.gameObject).GetComponent<Health>().take_damage(damage, DT: DamageType.Elemental, isDoT: true);
                }
            }
        }
    }
}
