using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsumableController : MonoBehaviour
{
    private Inventory Inventory;
    private Transform Quickslot_item;
    private Image ItemImage;

    void Start()
    {
        Inventory = GameObject.Find("Player").GetComponentInChildren<Inventory>();
        Quickslot_item = GameObject.Find("Quickslot_item").transform;
        ItemImage = Quickslot_item.Find("Image").GetComponent<Image>();
    }


    void Update()
    {
        GameObject equipedItem = Inventory.ReturnConsumable();
        if(equipedItem != null)
        {
            ItemImage.sprite = equipedItem.GetComponent<Consumable>().Item_sprite;
            float cd_remaining = equipedItem.GetComponent<Consumable>().cooldown_remaining;
            if (cd_remaining > 0)
            {
                Quickslot_item.Find("Text").GetComponent<TextMeshProUGUI>().text = cd_remaining.ToString("0");
                Quickslot_item.Find("Darken").gameObject.SetActive(true);
            }
            else
            {
                Quickslot_item.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
                Quickslot_item.Find("Darken").gameObject.SetActive(false);
            }
        }
        else
        {
            ItemImage.sprite = null;
            Quickslot_item.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
            Quickslot_item.Find("Darken").gameObject.SetActive(false);
        }
    }

    public void HandleConsumables()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            GameObject temp = Inventory.ReturnConsumable();
            if(temp != null)
            {
                if (temp.GetComponent<Consumable>().AttemptConsumable())
                {
                    Inventory.DisposeConsumable();
                }
            }
        }
    }
}
