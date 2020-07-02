using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform enemy = null;
    [SerializeField] int max_enemy = 1;
    [SerializeField] bool roam = false;
    [SerializeField] float X_range = 10;
    [SerializeField] float Z_range = 10;
    [SerializeField] float movement_cd = 4;
    [SerializeField] float cd_randomness = 2;
    [SerializeField] float roam_speed = 4;
    [SerializeField] float disable_range = 150;

    private Collider trigger;
    private GameObject player;
    private GameObject enemies;
    private bool spawner_enabled;

    void Start()
    {
        spawner_enabled = false;
        trigger = GetComponent<SphereCollider>();
        player = GameObject.Find("Player");
        enemies = GameObject.Find("AliveEnemies");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enable_disable(true);
        }
    }

    private void FixedUpdate()
    {
        if (spawner_enabled)
        {
            float distance = (player.transform.position - transform.position).magnitude;
            if(distance >= disable_range)
            {
                untether_active_enemies();
                enable_disable(false);
            }
        }
    }


    void enable_disable(bool switchVar)
    {
        if (switchVar)
        {
            spawner_enabled = true;
            Replenish_enemies();
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            spawner_enabled = false;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }



    void Replenish_enemies()
    {
        for (int i = transform.childCount; i < max_enemy; i++)
        {
            GameObject clone = Instantiate(enemy, transform.position, transform.rotation, transform).gameObject;
            clone.GetComponent<EnemyTemplateMaster>().SpawnEnemy(roam, transform, X_range, Z_range, movement_cd, cd_randomness, roam_speed);
        }
    }


    void untether_active_enemies()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<EnemyTemplateMaster>().AIenabled)
            {
                child.parent = enemies.transform;
            }
        }
    }
}
