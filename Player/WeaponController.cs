using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Inventory Inventory;
    private Transform Storage;

    private int currentWeapon;
    public GameObject ItemParent;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        Inventory = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        Storage = GameObject.Find("Storage").transform;
        currentWeapon = 0;
    }

    public void HandleWeapon()
    {
        ReadyWeapon();
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

            if(Inventory.ReturnWeapon(currentWeapon) != null)
            {
                CleanParenting();
            }
        }
    }

    // TODO: Handle input with input manager and not direct key references
    private void ReadyWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool AnyWeaponReady = false;
            if (Inventory.ReturnWeapon(0))
            {
                AnyWeaponReady = true;
                currentWeapon = 0;
            }
            else if (Inventory.ReturnWeapon(1))
            {
                AnyWeaponReady = true;
                currentWeapon = 1;
            }

            if (AnyWeaponReady)
            {
                if (playerMovement.itemEquipped)
                {
                    playerMovement.itemEquipped = false;
                    Transform tempObject = ItemParent.transform.GetChild(0);
                    tempObject.parent = Storage;
                    tempObject.gameObject.SetActive(false);
                }
                else
                {
                    playerMovement.itemEquipped = true;
                    CleanParenting();
                }
            }
        }
    }

    // TODO: Handle input with input manager and not direct key references
    private void SwitchWeapon()
    {
        if (playerMovement.itemEquipped && Input.GetKeyDown(KeyCode.Q))
        {
            int otherWeapon;
            if(currentWeapon == 0)
            {
                otherWeapon = 1;
            }
            else
            {
                otherWeapon = 0;
            }

            if (Inventory.ReturnWeapon(otherWeapon) != null)
            {
                Transform tempObject = ItemParent.transform.GetChild(0);
                tempObject.parent = Storage;
                tempObject.gameObject.SetActive(false);
                currentWeapon = otherWeapon;
                CleanParenting();
            }
        }
    }

    // TODO: Handle input with input manager and not direct key references
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

    private void CleanParenting() //Takes the global transform values and makes them into local values. This way Items can be placed precisely
    {
        GameObject TempWeapon = Inventory.ReturnWeapon(currentWeapon);
        TempWeapon.SetActive(true);
        TempWeapon.transform.SetParent(ItemParent.transform);
        Weapon tempWeapon = TempWeapon.GetComponent<Weapon>();

        TempWeapon.transform.localEulerAngles = tempWeapon.StartingRotation;
        TempWeapon.transform.localPosition = tempWeapon.StartingLocation;
        TempWeapon.transform.localScale = tempWeapon.StartingScale;
    }
}
