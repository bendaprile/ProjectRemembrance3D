using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crocodileAI : EnemyTemplateMaster
{
    [SerializeField] protected float charge_cooldown = 10f;
    [SerializeField] protected ChargingEnemy charge_script = null;

    private float next_charge;


    protected override void Start()
    {
        base.Start();
        next_charge = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            AIenabled = true;
            agent.speed = original_speed;
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

    protected override void AIFunc()
    {
        if ((next_charge <= timer) && charge_script.Charge())
        {
            cc_immune = true;
            rB.isKinematic = false;
            agent.enabled = false;
            next_charge = timer + charge_cooldown;
            animationUpdater.PlayAnimation("sprint");
        } 
        else if(!charge_script.isCharging)
        {
            cc_immune = false;
            base.AIFunc();
        }
    }

    public override void Death()
    {
        base.Death();
        Hitbox.GetComponent<Collider>().enabled = false;
    }
}
