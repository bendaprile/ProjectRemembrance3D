using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUIMenu : MonoBehaviour
{

    Inventory inv;
    GameObject InventoryMenu;

    Text MainWeaponText;
    Text SecondaryWeaponText;

    Transform MainStats;
    Transform SecondaryStats;

    void Start()
    {
        inv = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        InventoryMenu = GameObject.Find("InventoryMenu");

        MainWeaponText = GameObject.Find("MainWeaponText").GetComponent<Text>();
        SecondaryWeaponText = GameObject.Find("SecondaryWeaponText").GetComponent<Text>();

        MainStats = transform.Find("MainStats");
        SecondaryStats = transform.Find("SecondaryStats");

        UpdatePanel();
    }

    public void unequipMain()
    {
        inv.UnEquipWeapon(0);
        UpdatePanel();
        InventoryMenu.GetComponent<InventoryMenuController>().UpdateInventoryPanel();
    }

    public void unequipSecondary()
    {
        inv.UnEquipWeapon(1);
        UpdatePanel();
        InventoryMenu.GetComponent<InventoryMenuController>().UpdateInventoryPanel();
    }

    public void UpdatePanel()
    {
        MainWeaponText.text = "";
        foreach (Transform child in MainStats)
        {
            child.GetComponent<Text>().text = "";
        }

        if (inv.ReturnWeapon(0) != null)
        {
            ItemMaster mainItem = inv.ReturnWeapon(0).GetComponent<ItemMaster>();
            MainWeaponText.text = mainItem.ItemName;
            if(((Weapon)mainItem).weaponType == WeaponType.Melee_1H)
            {
                Melee1H temp = (Melee1H)mainItem;

                MainStats.Find("A_Name").GetComponent<Text>().text = "Basic Attack Damage: ";
                MainStats.Find("A_Value").GetComponent<Text>().text = temp.basicAttackDamage.ToString();

                MainStats.Find("B_Name").GetComponent<Text>().text = "Heavy Attack Damage: ";
                MainStats.Find("B_Value").GetComponent<Text>().text = temp.heavyAttackDamage.ToString();

                MainStats.Find("C_Name").GetComponent<Text>().text = "Energy Cost: ";
                MainStats.Find("C_Value").GetComponent<Text>().text = temp.energyCost.ToString();

                MainStats.Find("D_Name").GetComponent<Text>().text = "Damage Type: ";
                MainStats.Find("D_Value").GetComponent<Text>().text = temp.DT.ToString();
            }
        }



        SecondaryWeaponText.text = "";
        foreach (Transform child in SecondaryStats)
        {
            child.GetComponent<Text>().text = "";
        }
        if (inv.ReturnWeapon(1) != null)
        {
            ItemMaster secondaryItem = inv.ReturnWeapon(1).GetComponent<ItemMaster>();
            SecondaryWeaponText.text = secondaryItem.ItemName;
            if (((Weapon)secondaryItem).weaponType == WeaponType.Melee_1H)
            {
                Melee1H temp = (Melee1H)secondaryItem;

                SecondaryStats.Find("A_Name").GetComponent<Text>().text = "Basic Attack Damage: ";
                SecondaryStats.Find("A_Value").GetComponent<Text>().text = temp.basicAttackDamage.ToString();

                SecondaryStats.Find("B_Name").GetComponent<Text>().text = "Heavy Attack Damage: ";
                SecondaryStats.Find("B_Value").GetComponent<Text>().text = temp.heavyAttackDamage.ToString();

                SecondaryStats.Find("C_Name").GetComponent<Text>().text = "Energy Cost: ";
                SecondaryStats.Find("C_Value").GetComponent<Text>().text = temp.energyCost.ToString();

                SecondaryStats.Find("D_Name").GetComponent<Text>().text = "Damage Type: ";
                SecondaryStats.Find("D_Value").GetComponent<Text>().text = temp.DT.ToString();
            }
        }
    }
}
