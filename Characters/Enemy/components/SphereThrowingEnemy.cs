using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereThrowingEnemy : MonoBehaviour
{
    public GameObject lightningSphere = null;

    [SerializeField] private float radius = 0f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float max_duration = 0f;
    [SerializeField] private float damage = 0f;

    private Transform player;
    private Transform EnemyProjectiles;

    private void Start()
    {

        player = GameObject.Find("Player").transform;
        EnemyProjectiles = GameObject.Find("EnemyProjectiles").transform;
    }


    public bool Cast()
    {
        if (clean_LoS()) 
        {
            CastMechanics();
            return true;
        }
        return false;
    }

    public void CastMechanics()
    {
        Vector3 mod_transform = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);

        GameObject clone = Instantiate(lightningSphere, mod_transform, transform.rotation, EnemyProjectiles).gameObject;
        Rigidbody RB = clone.GetComponent<Rigidbody>();
        LightningSphereProjectile LSP_Script = clone.GetComponent<LightningSphereProjectile>();

        RB.velocity = transform.TransformDirection(Vector3.forward * speed);
        LSP_Script.duration = max_duration;
        LSP_Script.max_targets = 1;
        LSP_Script.damage = damage;
        LSP_Script.radius = radius;
        LSP_Script.enemy_strike = true;

        clone.transform.Find("Sphere").GetComponent<SphereCollider>().isTrigger = true;
        LSP_Script.maintain_speed = speed;
    }



    public bool clean_LoS() //Has direct Line of Sight to the player
    {
        int layerMask = (LayerMask.GetMask("Projectile") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("BasicEnemy"));
        layerMask = ~layerMask; //ignore these

        Vector3 player_dir = player.transform.position - transform.position;

        float player_angle = Mathf.Atan2(player_dir.x, player_dir.z) * Mathf.Rad2Deg;
        float forward_angle = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;

        float diff_angle = Mathf.Abs(Mathf.DeltaAngle(player_angle, forward_angle));

        if(diff_angle < 10)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player_dir, out hit, range, layerMask))
            {
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
}
