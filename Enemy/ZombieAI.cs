using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : EnemyTemplateMaster
{
    ZombieAttack zombieAttack;


    protected override void Start()
    {
        base.Start();
        zombieAttack = GetComponentInChildren<ZombieAttack>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void TakeDamageEnemy(Vector3 Force = new Vector3())
    {
        base.TakeDamageEnemy(Force);
        animationUpdater.PlayAnimation("no_attack", false, true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            AIenabled = true;
            agent.speed = original_speed;
        }
    }

    protected override void AIFunc()
    {
        base.AIFunc();
        if (!animationUpdater.disableMovement && zombieAttack.isAttacking)
        {
            animationUpdater.PlayAnimation("attack");
            zombieAttack.isAttacking = false;
        }
    }

    protected override void RoamingFunc()
    {
        base.RoamingFunc();
        if (agent.remainingDistance < 0.01f)
        {
            animationUpdater.PlayAnimation("idle");
        }
        else
        {
            animationUpdater.PlayAnimation("walk");
        }
    }

    public override void Death()
    {
        base.Death();
        animationUpdater.PlayAnimation("no_attack", false, true); //Stops attacks after death
        Hitbox.GetComponent<CapsuleCollider>().enabled = false;
    }

}
