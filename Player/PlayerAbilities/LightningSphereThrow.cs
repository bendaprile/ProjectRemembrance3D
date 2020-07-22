using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSphereThrow : Ability
{
    public GameObject lightningSphere;
    public Transform PlayerProjectiles;

    [SerializeField] private float speed = 0f;
    [SerializeField] private float max_duration = 0f;
    [SerializeField] private int max_targets = 0;
    [SerializeField] private float damage = 0f;

    [SerializeField] private bool turret_mode = false;
    [SerializeField] private bool bounce_mode = false;

    protected override void Attack()
    {
        Vector3 mod_transform = new Vector3 (transform.position.x, transform.position.y + .5f, transform.position.z);

        GameObject clone = Instantiate(lightningSphere, mod_transform, transform.rotation, PlayerProjectiles).gameObject;
        Rigidbody RB = clone.GetComponent<Rigidbody>();
        LightningSphereProjectile LSP_Script = clone.GetComponent<LightningSphereProjectile>();

        RB.velocity = transform.TransformDirection(Vector3.forward * speed);
        LSP_Script.duration = max_duration;
        LSP_Script.max_targets = max_targets;
        LSP_Script.damage = damage;
        LSP_Script.maintain_speed = 0f;
        LSP_Script.radius = 10f;
        LSP_Script.enemy_strike = false;

        if (turret_mode)
        {
            //clone.transform.Find("Sphere").GetComponent<SphereCollider>().isTrigger = false;
            RB.drag = speed / 15f;
        }

        if (bounce_mode)
        {
            clone.transform.Find("Sphere").GetComponent<SphereCollider>().isTrigger = false;
            LSP_Script.maintain_speed = speed;
        }
    }
}
