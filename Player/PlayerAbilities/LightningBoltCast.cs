using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltCast : Ability
{

    public GameObject lightningBolt;
    public Transform PlayerProjectiles;
    [SerializeField] private float energyCost = 120f;
    [SerializeField] private float damage = 4f;

    public float cdDelay;

    private Energy energy;
    private float usable_time;

    private void Start()
    {
        usable_time = 0f;
        energy = GameObject.Find("Player").GetComponent<Energy>();
    }

    public override void Attack(float time_dia, float master_counter)
    {
        if (master_counter >= usable_time && energy.drain_energy(energyCost))
        {
            usable_time = master_counter + cdDelay;

            GameObject clone = Instantiate(lightningBolt, SetPos(), transform.rotation, PlayerProjectiles).gameObject;
            LightningBoltProjectile LBP = clone.GetComponent<LightningBoltProjectile>();
            LBP.damage = damage;
            LBP.AttackDelay = 1f;
            LBP.time_dia = 1f;
            LBP.enemy_strike = false;
        }
    }

    private Vector3 SetPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (LayerMask.GetMask("Terrain"));
        RaycastHit hitray;
        Vector3 mousePos3d = new Vector3(0f, 0f, 0f);

        if (Physics.Raycast(ray, out hitray, Mathf.Infinity, layerMask))
        {
            mousePos3d = hitray.point;
        }

        return mousePos3d;
    }
}
