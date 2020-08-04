using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsHolder : MonoBehaviour
{
    private List<GameObject> CompletedQuests = new List<GameObject>();
    private List<GameObject> ActiveQuests = new List<GameObject>();


    private PlayerStats playerStats;

    public GameObject TestQuest;
    void Start()
    {
        //Testing only //NOTE if the quest is complete before the game starts it will not work properly. This is because the items in the inventory are not initialized yet.
        Transform tempPrefab = Instantiate(TestQuest.transform, transform);
        AddQuest(tempPrefab);
        //Testing only

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    public void AddQuest(Transform Quest)
    {
        ActiveQuests.Add(Quest.gameObject);
        Quest.GetComponent<QuestTemplate>().QuestStart();
    }

    public List<GameObject> ReturnActiveQuests()
    {
        return ActiveQuests;
    }

    public void FullQuestCompleted(string UniqueName)
    {
        int temp_loc = 0;
        foreach(GameObject iter in ActiveQuests)
        {
            QuestTemplate QuestiterScript = iter.GetComponent<QuestTemplate>();
            if (QuestiterScript.QuestName == UniqueName)
            {
                CompletedQuests.Add(iter);
                ActiveQuests.Remove(iter);
                playerStats.AddEXP(QuestiterScript.xp_reward);
                break;
            }
            temp_loc += 1;
        }
    }

    public void CheckFetchObjectives(GameObject item, bool itemAdd)
    {
        ItemMaster itemStats = item.GetComponent<ItemMaster>();

        for(int i = ActiveQuests.Count - 1; i >= 0; i--) //A quest could be completed
        {
            QuestObjective TempActiveObj = ActiveQuests[i].GetComponent<QuestTemplate>().returnActiveObjective();
            if (TempActiveObj.ReturnType() == ObjectiveType.Fetch)
            {
                ((FetchObjective)TempActiveObj).UpdateItemCount(itemStats, itemAdd);
            }
        }
    }
}
