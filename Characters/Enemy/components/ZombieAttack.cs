using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private bool canAttack;
    public bool isAttacking;

    [SerializeField] float attackCD = 2f;
    [SerializeField] float damagePerSwing = 10f;

    private Transform player;

    private float time;

    void Start()
    {
        time = 0f;
        canAttack = false;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if(canAttack && time >= attackCD)
        {
            isAttacking = true;
            time = 0;
        }
    }

    public void DealDamage()
    {
        if (canAttack) //Player is still in range
        {
            player.GetComponent<Health>().take_damage(damagePerSwing);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttack = true;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = false;
        }
    }

}
