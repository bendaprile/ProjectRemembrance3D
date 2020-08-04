using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchObjective : QuestObjective
{
    [SerializeField] private GameObject itemNeeded;
    private ItemMaster itemNeededSpecs;

    [SerializeField] private int itemsRequired;
    private int currentItems;

    public override void initialize()
    {
        base.initialize();

        currentItems = 0;
        itemNeededSpecs = itemNeeded.GetComponent<ItemMaster>();

        List<(int, GameObject)> temp = GameObject.Find("Inventory").GetComponent<Inventory>().ReturnItems(itemNeededSpecs.ItemType);

        foreach((int, GameObject) iter in temp)
        {
            if(iter.Item2.GetComponent<ItemMaster>().ItemName == itemNeededSpecs.ItemName)
            {
                currentItems += 1;
            }
        }

        CheckStatus();
    }

    protected override void CheckStatus()
    {
        if(currentItems >= itemsRequired)
        {
            questTemplate.ObjectiveFinished();
        }
    }

    public void UpdateItemCount(ItemMaster itemStats, bool itemAdd)
    {
        if (itemStats.ItemName == itemNeededSpecs.ItemName)
            if (itemAdd)
            {
                currentItems += 1;
            }
            else
            {
                currentItems -= 1;
            }
        CheckStatus();
    }

    public override string ReturnDescription()
    {
        string amount_completed = " (" + currentItems.ToString() + "/" + itemsRequired + ")";
        return (Description + amount_completed);
    }

    public override ObjectiveType ReturnType()
    {
        return ObjectiveType.Fetch;
    }
}
