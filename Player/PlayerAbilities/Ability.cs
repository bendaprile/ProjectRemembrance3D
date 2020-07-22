using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private float cooldown = 0f;
    [SerializeField] public Sprite ability_sprite;
    [SerializeField] protected float energyCost = 0f;
    public float cooldown_remaining;

    protected Energy energy;

    public Ability()
    {

    }

    void Awake()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();
        cooldown_remaining = 0f;
    }

    private void FixedUpdate()
    {
        if(cooldown_remaining > 0)
        {
            cooldown_remaining -= Time.fixedDeltaTime;
        }
    }

    public virtual void AttemptAttack()
    {
        if (cooldown_remaining <= 0f)
        {
            if (energy.drain_energy(energyCost))
            {
                cooldown_remaining = cooldown;
                Attack();
            }
        }
    }

    protected virtual void Attack()
    {

    }

    public virtual void AttackFinished()
    {

    }
}
