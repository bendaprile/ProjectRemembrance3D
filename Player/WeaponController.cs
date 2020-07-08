using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Inventory Inventory;
    
    private bool currentWeapon;
    public GameObject ItemParent;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        Inventory = GameObject.Find("Player").GetComponent<Inventory>();
        currentWeapon = false;
    }

    public void HandleWeapon()
    {
        EquipWeapon();
        SwitchWeapon();

        if (playerMovement.itemEquipped && playerMovement.moveState != PlayerMovement.MoveState.Rolling)
        {
            Attack();
        }
    }

    public void RefreshWeapons()
    {
        if (playerMovement.itemEquipped)
        {
            foreach (Transform child in ItemParent.transform)
            {
                Destroy(child.gameObject);
            }

            if(Inventory.ReturnWeapon(BoolToInt(currentWeapon)) != null)
            {
                CleanParenting();
            }
        }
    }

    private void EquipWeapon()
    {
        bool AnyWeaponReady = false;
        if (Inventory.ReturnWeapon(0))
        {
            AnyWeaponReady = true;
            currentWeapon = false;
        }
        else if (Inventory.ReturnWeapon(1))
        {
            AnyWeaponReady = true;
            currentWeapon = true;
        }


        if (Input.GetKeyDown(KeyCode.Z) && AnyWeaponReady)
        {
            if (playerMovement.itemEquipped)
            {
                playerMovement.itemEquipped = false;
                foreach (Transform child in ItemParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                playerMovement.itemEquipped = true;
                CleanParenting();
            }
        }
    }

    private void SwitchWeapon()
    {
        if (playerMovement.itemEquipped && Input.GetKeyDown(KeyCode.Q) && Inventory.ReturnWeapon(BoolToInt(!currentWeapon)) != null) //! for the other weapon
        {
            foreach(Transform child in ItemParent.transform)
            {
                Destroy(child.gameObject);
            }

            currentWeapon = !currentWeapon;
            CleanParenting();
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ItemParent.GetComponentInChildren<Weapon>().weaponType == WeaponType.Melee_1H)
            {
                ItemParent.GetComponentInChildren<Weapon>().Attack();
            }
        }
    }

    public void DealDamage()
    {
        ItemParent.GetComponentInChildren<Weapon>().DealDamage();
    }

    public void AttackFinished()
    {
        ItemParent.GetComponentInChildren<Weapon>().AttackFinished();
    }

    private int BoolToInt(bool input)
    {
        if (input)
        {
            return 1;
        }
        return 0;
    }

    private void CleanParenting() //Takes the global transform values and makes them into local values. This way Items can be placed precisely
    {
        GameObject TempWeapon = Instantiate(Inventory.ReturnWeapon(BoolToInt(currentWeapon)));
        Weapon tempWeapon = TempWeapon.GetComponent<Weapon>();
        TempWeapon.transform.SetParent(ItemParent.transform);

        TempWeapon.transform.localEulerAngles = tempWeapon.StartingRotation;
        TempWeapon.transform.localPosition = tempWeapon.StartingLocation;
        TempWeapon.transform.localScale = tempWeapon.StartingScale;
    }
}
