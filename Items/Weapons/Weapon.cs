using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemMaster
{

    [SerializeField] public WeaponType weaponType;

    [SerializeField] public DamageType DT = DamageType.Regular;

    [SerializeField] public Vector3 StartingLocation = new Vector3();
    [SerializeField] public Vector3 StartingRotation = new Vector3();
    [SerializeField] public Vector3 StartingScale = new Vector3();

    public Weapon()
    {

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
