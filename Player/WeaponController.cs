using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameObject[] EquipedWeapons = new GameObject[2];
    
    private bool currentWeapon;
    public GameObject ItemParent;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        EquipedWeapons = GameObject.Find("Player").GetComponent<Inventory>().EquipedWeapons;
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

    private void EquipWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z) && EquipedWeapons[0] != null)
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
        if (playerMovement.itemEquipped && Input.GetKeyDown(KeyCode.Q) && EquipedWeapons[1] != null) 
        {
            foreach(Transform child in ItemParent.transform)
            {
                Destroy(child.gameObject);
            }

            currentWeapon = !currentWeapon;
            CleanParenting();
        }
    }

    public void PickupWeapon()
    {

    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ItemParent.GetComponentInChildren<Weapon>().weaponType == Weapon.WeaponType.Melee_1H)
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

    private void CleanParenting()
    {
        GameObject TempWeapon = Instantiate(EquipedWeapons[BoolToInt(currentWeapon)]);
        Transform tempTrans = TempWeapon.transform;
        TempWeapon.transform.SetParent(ItemParent.transform);

        TempWeapon.transform.localEulerAngles = tempTrans.eulerAngles;
        TempWeapon.transform.localPosition = tempTrans.position;
        TempWeapon.transform.localScale = tempTrans.lossyScale;
    }
}
