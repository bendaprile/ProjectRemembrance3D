using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractiveUIPrefab : MonoBehaviour
{
    private InteractiveObjectMenuUI IOM;
    private ItemMaster ITEM;
    private int itemStorageLoc;

    public void Setup(ItemMaster ITEM_in, int storage_loc)
    {
        IOM = GameObject.Find("InteractiveObjectMenu").GetComponent<InteractiveObjectMenuUI>();
        ITEM = ITEM_in;

        transform.Find("NameText").gameObject.GetComponent<Text>().text = ITEM.ItemName;
        transform.Find("ValueText").gameObject.GetComponent<Text>().text = ITEM.Cost.ToString();
        transform.Find("MassText").gameObject.GetComponent<Text>().text = ITEM.Weight.ToString();
        itemStorageLoc = storage_loc;
    }

    public void ButtonPressedTransfer()
    {
        IOM.TransferButtonPressed(itemStorageLoc);
    }

    public void ButtonPressedStats()
    {
        IOM.StatsButtonPressed(ITEM);
    }
}