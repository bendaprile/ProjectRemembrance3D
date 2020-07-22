using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightningCrocodileAI : EnemyTemplateMaster
{
    [SerializeField] protected float sphere_cast_cooldown = 10f;
    [SerializeField] protected float lightning_cast_cooldown = 10f;
    [SerializeField] protected SphereThrowingEnemy sphere_script = null;
    [SerializeField] protected LightningCastingEnemy lightning_script = null;

    private float sphere_next_cast;
    private float lightning_next_cast;


    protected override void Start()
    {
        base.Start();
        sphere_next_cast = 0;
        lightning_next_cast = 0;
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
        if ((sphere_next_cast <= timer) && sphere_script.clean_LoS())
        {
            animationUpdater.PlayAnimation("sphere_attack");
            agent.enabled = false;
            sphere_next_cast = timer + sphere_cast_cooldown;
        }
        else if (lightning_next_cast <= timer && lightning_script.attack())
        {
            lightning_next_cast = timer + lightning_cast_cooldown;
        }
        else if (!animationUpdater.genericBool0)
        {
            base.AIFunc();
        }
    }

    public override void AnimationCalledFunc0() //Fires in the middle of the animation
    {
        sphere_script.CastMechanics();
    }

    public override void Death()
    {
        base.Death();
        Hitbox.GetComponent<Collider>().enabled = false;
    }
}
