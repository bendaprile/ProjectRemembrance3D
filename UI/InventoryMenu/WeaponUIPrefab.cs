using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIPrefab : MonoBehaviour
{
    private Inventory inv;
    private InventoryMenuController InventoryMenu;
    private WeaponsUIMenu WUIM;
    private RectTransform mainTransform;
    private GameObject LocalStatsMenu;

    private int itemStorageLoc;

    public void Setup(Weapon itemProperties, int storage_loc)
    {
        inv = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        InventoryMenu = GameObject.Find("InventoryMenu").GetComponent<InventoryMenuController>();
        WUIM = GameObject.Find("WeaponsMenu").GetComponent<WeaponsUIMenu>();
        LocalStatsMenu = transform.Find("InventoryItemStatsMenu").gameObject;
        mainTransform = GetComponent<RectTransform>();

        transform.Find("ItemName").GetComponent<Text>().text = itemProperties.ItemName;
        transform.Find("ItemCost").GetComponent<Text>().text = itemProperties.Cost.ToString();
        transform.Find("ItemWeight").GetComponent<Text>().text = itemProperties.Weight.ToString();
        itemStorageLoc = storage_loc;

        Transform InnerBoxLeft = LocalStatsMenu.transform.Find("InnerBoxLeft");

        if (itemProperties.weaponType == WeaponType.Melee_1H)
        {
            Melee1H temp = (Melee1H)itemProperties;

            InnerBoxLeft.Find("A_Name").GetComponent<Text>().text = "Basic Attack Damage: ";
            InnerBoxLeft.Find("A_Value").GetComponent<Text>().text = temp.basicAttackDamage.ToString();

            InnerBoxLeft.Find("B_Name").GetComponent<Text>().text = "Heavy Attack Damage: ";
            InnerBoxLeft.Find("B_Value").GetComponent<Text>().text = temp.heavyAttackDamage.ToString();

            InnerBoxLeft.Find("C_Name").GetComponent<Text>().text = "Energy Cost: ";
            InnerBoxLeft.Find("C_Value").GetComponent<Text>().text = temp.energyCost.ToString();

            InnerBoxLeft.Find("D_Name").GetComponent<Text>().text = "Damage Type: ";
            InnerBoxLeft.Find("D_Value").GetComponent<Text>().text = temp.DT.ToString();
        }


    }

    public void DisplayStats()
    {
        if (LocalStatsMenu.activeSelf)
        {
            LocalStatsMenu.SetActive(false);
            mainTransform.sizeDelta = new Vector2(mainTransform.sizeDelta.x, mainTransform.sizeDelta.y / 3);
        }
        else
        {
            LocalStatsMenu.SetActive(true);
            mainTransform.sizeDelta = new Vector2(mainTransform.sizeDelta.x, mainTransform.sizeDelta.y * 3);
        }
    }

    public void DeleteSelectedItem()
    {
        inv.DeleteItem(itemStorageLoc);
        InventoryMenu.UpdateInventoryPanel();
    }

    public void EquipMain()
    {
        inv.EquipWeapon(itemStorageLoc, 0);
        InventoryMenu.UpdateInventoryPanel();
        WUIM.UpdatePanel();
    }

    public void EquipSecondary()
    {
        inv.EquipWeapon(itemStorageLoc, 1);
        InventoryMenu.UpdateInventoryPanel();
        WUIM.UpdatePanel();
    }
}
