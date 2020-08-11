using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUIPrefab : MonoBehaviour
{
    private Inventory inv;
    private InventoryMenuController InventoryMenu;
    private ConsumableUIMenu CUIM;
    private GameObject ItemStats;
    private RectTransform mainTransform;

    private int itemStorageLoc;

    public void Setup(Consumable itemProperties, int storage_loc)
    {
        inv = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        InventoryMenu = GameObject.Find("InventoryMenu").GetComponent<InventoryMenuController>();
        CUIM = GameObject.Find("ConsumableMenu").GetComponent<ConsumableUIMenu>();
        ItemStats = transform.Find("InventoryItemStatsMenu").gameObject;
        mainTransform = GetComponent<RectTransform>();

        transform.Find("ItemName").GetComponent<Text>().text = itemProperties.ItemName;
        transform.Find("ItemCost").GetComponent<Text>().text = itemProperties.Cost.ToString();
        transform.Find("ItemWeight").GetComponent<Text>().text = itemProperties.Weight.ToString();
        transform.Find("ItemQuanity").GetComponent<Text>().text = ("x" + itemProperties.quantity.ToString());
        itemStorageLoc = storage_loc;

        Transform InnerBoxLeft = ItemStats.transform.Find("InnerBoxLeft");

        if (itemProperties.consumableType == ConsumableType.Restoring)
        {
            Restoring temp = (Restoring)itemProperties;

            InnerBoxLeft.Find("A_Name").GetComponent<Text>().text = "Health Restore Amount: ";
            InnerBoxLeft.Find("A_Value").GetComponent<Text>().text = temp.health_restore_amount.ToString();

            InnerBoxLeft.Find("B_Name").GetComponent<Text>().text = "Energy Restore Amount: ";
            InnerBoxLeft.Find("B_Value").GetComponent<Text>().text = temp.energy_restore_amount.ToString();

            InnerBoxLeft.Find("C_Name").GetComponent<Text>().text = "Duration: ";
            if (temp.instant)
            {
                InnerBoxLeft.Find("C_Value").GetComponent<Text>().text = "Instant";
            }
            else
            {
                InnerBoxLeft.Find("C_Value").GetComponent<Text>().text = temp.duration.ToString();
            }

            InnerBoxLeft.Find("D_Name").GetComponent<Text>().text = "Cooldown: ";
            InnerBoxLeft.Find("D_Value").GetComponent<Text>().text = temp.cooldown.ToString();
        }

        if (itemProperties.LockForQuest)
        {
            transform.Find("ItemDelete").GetComponent<Button>().interactable = false;
            transform.Find("ItemEquip").GetComponent<Button>().interactable = false;
        }
    }

    public void DisplayStats()
    {
        if (ItemStats.activeSelf)
        {
            ItemStats.SetActive(false);
            mainTransform.sizeDelta = new Vector2(mainTransform.sizeDelta.x, mainTransform.sizeDelta.y / 3);
        }
        else
        {
            ItemStats.SetActive(true);
            mainTransform.sizeDelta = new Vector2(mainTransform.sizeDelta.x, mainTransform.sizeDelta.y * 3);
        }
    }

    public void DeleteSelectedItem()
    {
        inv.DeleteItem(itemStorageLoc);
        InventoryMenu.UpdateInventoryPanel();
    }

    public void Equip()
    {
        inv.EquipConsumable(itemStorageLoc); 
        InventoryMenu.UpdateInventoryPanel();
        CUIM.UpdatePanel();
    }
}
