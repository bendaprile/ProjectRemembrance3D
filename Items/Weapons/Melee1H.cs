using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee1H : Weapon
{
    [SerializeField] public float energyCost = 20f;
    [SerializeField] public float selfForce = 400f;
    [SerializeField] public float comboTimeout = 1f;

    [SerializeField] private float basicAttackForce = 400f;
    [SerializeField] private float heavyAttackForce = 600f;

    [SerializeField] public float basicAttackDamage = 30f;
    [SerializeField] public float heavyAttackDamage = 60f;

    private Energy energy;

    PlayerAnimationUpdater animationUpdater;
    PlayerMovement playerMovement;
    BoxCollider attackCollider;
    MeleeContact MC;

    List<string> animNames = new List<string>(new string[] {
        "melee_1h_attack_1",
        "melee_1h_attack_2",
        "melee_1h_attack_3" });

    private int attackAnimIndex = 0;
    private float comboTimer = 10f;
    private bool attacking = false;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        animationUpdater = GetComponentInParent<PlayerAnimationUpdater>();
        MC = GameObject.Find("MeleeHitbox").GetComponent<MeleeContact>();
        energy = GameObject.Find("Player").GetComponent<Energy>();
    }

    public Melee1H()
    {
        weaponType = WeaponType.Melee_1H;
    }

    private void FixedUpdate()
    {
        ComboTimer();
    }

    void ComboTimer()
    {
        if (!attacking)
        {
            comboTimer += Time.deltaTime;
        }

        if (comboTimer > comboTimeout)
        {
            attackAnimIndex = 0;
        }
    }

    public override void Attack()
    {
        if (!attacking && energy.drain_energy(energyCost))
        {
            playerMovement.StopMovement();
            playerMovement.moveState = PlayerMovement.MoveState.Melee;
            playerMovement.AddForwardForce(selfForce);

            attacking = true;
            animationUpdater.PlayAnimation(animNames[attackAnimIndex], true);
        }
    }

    public override void DealDamage()
    {
        bool AOE_smash = (attackAnimIndex == 2);
        if (AOE_smash)
        {
            MC.MeleeContactFunc(true, heavyAttackForce, heavyAttackDamage);
        }
        else
        {
            MC.MeleeContactFunc(false, basicAttackForce, basicAttackDamage);
        }
    }

    public override void AttackFinished()
    {
        attackAnimIndex++;
        comboTimer = 0;
        animationUpdater.DisableRootMotion();
        playerMovement.postAnimation = true;
        attacking = false;

        if (attackAnimIndex >= animNames.Count)
        {
            attackAnimIndex = 0;
        }
    }
}
