using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechAI : EnemyTemplateMaster
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.name == "Player")
        {

            AIenabled = true;
            agent.speed = original_speed;
        }
    }

    protected override void AIFunc()
    {
        base.AIFunc();
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
        Hitbox.GetComponent<CapsuleCollider>().enabled = false;
    }
}
