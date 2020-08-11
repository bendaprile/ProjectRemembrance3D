using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FetchObjective : QuestObjective
{
    [SerializeField] private List<GameObject> itemNeeded = new List<GameObject>();
    [SerializeField] private List<int> itemsRequired = new List<int>();
    [SerializeField] private List<string> Descriptions = new List<string>();
    private List<ItemMaster> itemNeededSpecs = new List<ItemMaster>();

    private List<int> currentItems = new List<int>();
    private List<int> itemLocs = new List<int>();

    private Inventory inventory;

    public override void initialize()
    {
        Assert.AreEqual(NumberOfTasks, itemNeeded.Count);
        Assert.AreEqual(itemNeeded.Count, itemsRequired.Count);
        Assert.AreEqual(itemsRequired.Count, Descriptions.Count);


        base.initialize();

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        for (int i = 0; i < NumberOfTasks; i++)
        {
            currentItems.Add(0);
            itemNeededSpecs.Add(itemNeeded[i].GetComponent<ItemMaster>());

            List<(int, GameObject)> temp = inventory.ReturnItems(itemNeededSpecs[i].ItemType);

            foreach ((int, GameObject) iter in temp)
            {
                if (iter.Item2.GetComponent<ItemMaster>().ItemName == itemNeededSpecs[i].ItemName && currentItems[i] < itemsRequired[i])
                {
                    currentItems[i] += 1;
                    itemLocs.Add(iter.Item1);
                    inventory.LockItemQuest(iter.Item1);
                }
            }

        }
        CheckStatus();
    }

    public List<int> ReturnItemLocs()
    {
        return itemLocs;
    }

    public void UpdateItemCount(ItemMaster itemStats, int location)
    {
        for (int i = 0; i < NumberOfTasks; i++)
        {
            if (itemStats.ItemName == itemNeededSpecs[i].ItemName && currentItems[i] < itemsRequired[i])
            {
                currentItems[i] += 1;
                itemLocs.Add(location);
                inventory.LockItemQuest(location);
            }
        }

        CheckStatus();
    }

    public override List<(bool, string)> ReturnTasks()
    {
        List<(bool, string)> taskList = new List<(bool, string)>();

        for (int i = 0; i < NumberOfTasks; i++)
        {
            string amount_completed = " (" + currentItems[i] + "/" + itemsRequired[i] + ")";
            taskList.Add((TaskComplete(i), (Descriptions[i] + amount_completed)));
        }

        return taskList;
    }

    protected override bool TaskComplete(int i)
    {
        return currentItems[i] >= itemsRequired[i];
    }

    public override ObjectiveType ReturnType()
    {
        return ObjectiveType.Fetch;
    }
}
