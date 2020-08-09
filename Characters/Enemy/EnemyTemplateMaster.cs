using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTemplateMaster : MonoBehaviour
{
    [SerializeField] protected float destroyAfterDeathDelay = 60f;
    [SerializeField] bool stunable = true;
    [SerializeField] bool knockbackable = true;
    [SerializeField] public string EnemyTypeName;
    public bool AIenabled;

    protected GameObject player;
    protected Rigidbody rB;
    protected EnemyAnimationUpdater animationUpdater;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Health health;
    protected Transform Hitbox;

    // Roam logic
    protected float roam_speed = 0;
    protected bool roam = false;
    protected Transform Spawner_Transform = null;
    protected float roam_rangeX = 0;
    protected float roam_rangeZ = 0;
    protected float roam_cd = 0;
    protected float cd_randomness = 0;
    protected float roam_time_tracker = 0;
    // Roam logic

    protected int exp_reward;
    protected bool cc_immune;
    protected float StunRelease;
    protected float timer;
    protected float original_speed;
    protected float current_speed;
    protected Transform deadEnemyParent;

    public EnemyTemplateMaster()
    {

    }



    public virtual void SpawnEnemy(bool Roam_in, Transform Spawner_Transform_in, float Roam_RangeX_in, float Roam_RangeZ_in, float Roam_cd_in, float cd_randomness_in, float roam_speed_in)
    {
        roam = Roam_in;
        Spawner_Transform = Spawner_Transform_in;
        roam_rangeX = Roam_RangeX_in;
        roam_rangeZ = Roam_RangeZ_in;
        roam_cd = Roam_cd_in;
        cd_randomness = cd_randomness_in;
        roam_speed = roam_speed_in;
    }



    public virtual void EnemyKnockback(Vector3 Force)
    {
        if (cc_immune || !knockbackable)
        {
            return;
        }
        agent.enabled = false;
        rB.isKinematic = false;
        animationUpdater.PlayFullAnimation("take_damage", true, true);
        rB.AddForce(Force);
    }

    public virtual void EnemyStun(float duration)
    {
        if (cc_immune || !stunable)
        {
            return;
        }
        agent.enabled = false;
        rB.isKinematic = false;
        StunRelease = timer + duration;
    }


    public virtual void Death()
    {
        player.GetComponent<PlayerStats>().AddEXP(exp_reward);
        player.GetComponentInChildren<QuestsHolder>().CheckGenericKillObjectives(transform.gameObject, new Vector2(transform.position.x, transform.position.y));

        // Freeze the position and remove collision of the enemy
        rB.constraints = RigidbodyConstraints.FreezeAll;
        rB.angularVelocity = Vector3.zero;
        animationUpdater.PlayAnimation("death", false, true);
        agent.enabled = false;

        // Makes it so the dead enemy cannot be targeted like an alive one
        Hitbox.tag = "DeadEnemy";

        transform.parent = deadEnemyParent;
        Destroy(gameObject, destroyAfterDeathDelay);
    }



    public virtual void AnimationCalledFunc0()
    {

    }



    protected virtual void Start()
    {
        cc_immune = false;
        timer = 0f;
        player = GameObject.Find("Player");
        Hitbox = transform.Find("Hitbox");
        deadEnemyParent = GameObject.Find("DeadEnemies").transform;

        AIenabled = false;
        rB = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animationUpdater = GetComponentInChildren<EnemyAnimationUpdater>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponentInChildren<Health>();

        original_speed = agent.speed;
    }



    protected virtual void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (rB.isKinematic)
        {
            current_speed = agent.velocity.magnitude;
        }
        else
        {
            current_speed = rB.velocity.magnitude;
        }
        animator.SetFloat("MoveSpeed", current_speed);



        if (health.isDead)
        {
            if (Hitbox.tag != "DeadEnemy")
            {
                Death(); //Seperated so the "else if" isn't actiavated
            }
        }
        else if(animationUpdater.disableMovement || StunRelease > timer) //knockback or stun
        {
            return;
        }
        else if (AIenabled)
        {
            AIFunc();
        }
        else if (roam)
        {
            RoamingFunc();
        }
    }



    protected virtual void AIFunc()
    {
        rB.isKinematic = true;
        agent.enabled = true;
        agent.SetDestination(player.transform.position);
        animationUpdater.PlayAnimation("run");
    }



    protected virtual void RoamingFunc()
    {
        rB.isKinematic = true;
        agent.speed = roam_speed;
        if (roam_time_tracker <= 0)
        {
            roam_time_tracker = roam_cd + Random.Range(-cd_randomness, cd_randomness); ;
            agent.enabled = true;
            Vector3 roam_dest = Spawner_Transform.position;
            roam_dest.x += Random.Range(-roam_rangeX, roam_rangeX);
            roam_dest.z += Random.Range(-roam_rangeZ, roam_rangeZ);
            agent.SetDestination(roam_dest);
        }
        else
        {
            roam_time_tracker -= Time.fixedDeltaTime;
        }
    }
}
