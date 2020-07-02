using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemMaster
{

    public WeaponType weaponType;

    public Weapon()
    {

    }

    public enum WeaponType
    {
        Melee_1H,
        Melee_2H,
        Gun_1H,
        Gun_2H
    }

    public virtual void Attack()
    {

    }

    public virtual void DealDamage()
    {

    }

    public virtual void AttackFinished()
    {

    }
}
