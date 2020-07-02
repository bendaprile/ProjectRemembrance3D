using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCastingEnemy : MonoBehaviour
{

    public GameObject lightningBolt;
    private Transform EnemyProjectiles;
    private Transform Player;

    [SerializeField] public float y_adjust = -1.2f;

    public float range;
    public int burst_amount;
    public float scatter_range;
    public float damage;
    public float AttackDelay;

    void Start()
    {
        EnemyProjectiles = GameObject.Find("EnemyProjectiles").transform;
        Player = GameObject.Find("Player").transform;
    }

    public bool attack()
    {
        if (player_in_range())
        {
            StartCoroutine(attackMechanics());
            return true;
        }
        return false;
    }

    private bool player_in_range()
    {
        Vector3 distance_vector = Player.position - transform.position;
        return (distance_vector.magnitude < range);
    }


    private IEnumerator attackMechanics()
    {
        for(int i = 0; i < burst_amount; i++)
        {
            Vector3 cast_pos = new Vector3(Player.position.x + Random.Range(-scatter_range, scatter_range), Player.position.y + y_adjust, Player.position.z + Random.Range(-scatter_range, scatter_range));

            GameObject clone = Instantiate(lightningBolt, cast_pos, transform.rotation, EnemyProjectiles).gameObject;
            LightningBoltProjectile LBP = clone.GetComponent<LightningBoltProjectile>();
            LBP.damage = damage;
            LBP.AttackDelay = AttackDelay;
            LBP.time_dia = 1f;
            LBP.enemy_strike = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
