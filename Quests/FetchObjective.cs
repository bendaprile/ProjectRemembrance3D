using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchObjective : QuestObjective //WILL BREAK IF ITEM IS EQUIPPED
{
    [SerializeField] private List<GameObject> itemNeeded = new List<GameObject>();
    [SerializeField] private List<int> itemsRequired = new List<int>();
    [SerializeField] private List<string> Descripions = new List<string>();
    private List<ItemMaster> itemNeededSpecs = new List<ItemMaster>();

    private List<int> currentItems = new List<int>();
    private List<int> itemLocs = new List<int>();

    public override void initialize()
    {
        base.initialize();

        for(int i = 0; i < itemNeeded.Count; i++)
        {
            currentItems.Add(0);
            itemNeededSpecs.Add(itemNeeded[i].GetComponent<ItemMaster>());

            List<(int, GameObject)> temp = GameObject.Find("Inventory").GetComponent<Inventory>().ReturnItems(itemNeededSpecs[i].ItemType);

            foreach ((int, GameObject) iter in temp)
            {
                if (iter.Item2.GetComponent<ItemMaster>().ItemName == itemNeededSpecs[i].ItemName)
                {
                    currentItems[i] += 1;
                }
            }

        }

        CheckStatus();
    }

    protected override void CheckStatus()
    {
        bool objFinished = true;
        for(int i = 0; i < itemNeeded.Count; i++)
        {
            if (currentItems[i] < itemsRequired[i])
            {
                objFinished = false;
            }
        }

        if(objFinished)
        {
            questTemplate.ObjectiveFinished();
        }
    }

    public void UpdateItemCount(ItemMaster itemStats, bool itemAdd, int location)
    {
        for (int i = 0; i < itemNeeded.Count; i++)
        {
            if (itemStats.ItemName == itemNeededSpecs[i].ItemName)
            {
                if (itemAdd)
                {
                    currentItems[i] += 1;
                    itemLocs.Add(location);
                }
                else
                {
                    currentItems[i] -= 1;
                    itemLocs.Remove(location); //Removes this particular item storage loc, not the list loc
                }
            }
        }

        CheckStatus();
    }

    public override List<(bool, string)> ReturnTasks()
    {
        List<(bool, string)> taskList = new List<(bool, string)>();

        for (int i = 0; i < itemNeeded.Count; i++)
        {
            string amount_completed = " (" + currentItems[i] + "/" + itemsRequired[i] + ")";
            taskList.Add(((currentItems[i] >= itemsRequired[i]), (Descripions[i] + amount_completed)));
        }

        return taskList;
    }

    public override ObjectiveType ReturnType()
    {
        return ObjectiveType.Fetch;
    }
}
