using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumable : ItemMaster
{
    [SerializeField] public ConsumableType consumableType;
    [SerializeField] public float cooldown;
    [SerializeField] public int quantity;

    public float cooldown_remaining;
    public Consumable()
    {

    }

    void Awake()
    {
        cooldown_remaining = 0f;
    }

    protected virtual void FixedUpdate()
    {
        if (cooldown_remaining > 0)
        {
            cooldown_remaining -= Time.fixedDeltaTime;
        }
    }

    public virtual bool AttemptConsumable()
    {
        if (cooldown_remaining <= 0f)
        {
            cooldown_remaining = cooldown;
            UseConsumable();
            return true;
        }
        return false;
    }

    public virtual void UseConsumable() //The quanity is updated in the ConsumableController
    {

    }
}
