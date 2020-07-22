using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUIMenu : MonoBehaviour
{
    Inventory inv;
    GameObject InventoryMenu;
    Text EquppiedConsText;
    Transform Stats;
    Image EquippedImage;

    void Start()
    {
        inv = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        EquppiedConsText = transform.Find("ConsumableName").GetComponent<Text>();
        EquippedImage = transform.Find("Image").GetComponent<Image>();
        Stats = transform.Find("Stats");
        InventoryMenu = GameObject.Find("InventoryMenu");
        UpdatePanel();
    }

    public void unequipCons()
    {
        inv.UnEquipConsumable();
        UpdatePanel();
        InventoryMenu.GetComponent<InventoryMenuController>().UpdateInventoryPanel();
    }

    public void UpdatePanel()
    {
        EquppiedConsText.text = "";
        EquippedImage.sprite = null;
        foreach (Transform child in Stats)
        {
            child.GetComponent<Text>().text = "";
        }

        if (inv.ReturnConsumable() != null)
        {
            Consumable Item = inv.ReturnConsumable().GetComponent<Consumable>();
            EquippedImage.sprite = Item.Item_sprite;
            EquppiedConsText.text = Item.ItemName;
            if (Item.consumableType == ConsumableType.Restoring)
            {
                Restoring temp = (Restoring)Item;

                Stats.Find("A_Name").GetComponent<Text>().text = "Health Restore Amount: ";
                Stats.Find("A_Value").GetComponent<Text>().text = temp.health_restore_amount.ToString();

                Stats.Find("B_Name").GetComponent<Text>().text = "Energy Restore Amount: ";
                Stats.Find("B_Value").GetComponent<Text>().text = temp.energy_restore_amount.ToString();

                Stats.Find("C_Name").GetComponent<Text>().text = "Duration: ";
                if (temp.instant)
                {
                    Stats.Find("C_Value").GetComponent<Text>().text = "Instant";
                }
                else
                {
                    Stats.Find("C_Value").GetComponent<Text>().text = temp.duration.ToString();
                }

                Stats.Find("D_Name").GetComponent<Text>().text = "Cooldown: ";
                Stats.Find("D_Value").GetComponent<Text>().text = temp.cooldown.ToString();

                Stats.Find("E_Name").GetComponent<Text>().text = "Quantity: ";
                Stats.Find("E_Value").GetComponent<Text>().text = temp.quantity.ToString();
            }
        }
    }
}
