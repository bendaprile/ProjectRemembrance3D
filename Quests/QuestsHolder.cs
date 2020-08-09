using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsHolder : MonoBehaviour
{
    private List<GameObject> CompletedQuests = new List<GameObject>();
    private List<GameObject> ActiveQuests = new List<GameObject>();

    private EventQueue eventQueue;
    private PlayerStats playerStats;
    private EventData tempEvent;


    void Start()
    {
        tempEvent = new EventData();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        eventQueue = GameObject.Find("EventDisplay").GetComponent<EventQueue>();
    }

    public void AddQuest(Transform Quest)
    {

        QuestTemplate tempQuest = Quest.GetComponent<QuestTemplate>();
        tempEvent.Setup(EventTypeEnum.QuestStarted, tempQuest.QuestName);
        eventQueue.AddEvent(tempEvent);
        Quest.parent = transform;
        ActiveQuests.Add(Quest.gameObject);
        tempQuest.QuestStart();
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

                tempEvent.Setup(EventTypeEnum.QuestCompleted, UniqueName);
                eventQueue.AddEvent(tempEvent);
                break;
            }
            temp_loc += 1;
        }
    }

    public void CheckFetchObjectives(GameObject item, bool itemAdd, int location)
    {
        ItemMaster itemStats = item.GetComponent<ItemMaster>();

        for(int i = ActiveQuests.Count - 1; i >= 0; i--) //A quest could be completed
        {
            QuestObjective TempActiveObj = ActiveQuests[i].GetComponent<QuestTemplate>().returnActiveObjective();
            if (TempActiveObj.ReturnType() == ObjectiveType.Fetch)
            {
                ((FetchObjective)TempActiveObj).UpdateItemCount(itemStats, itemAdd, location);
            }
        }
    }

    public void CheckGenericKillObjectives(GameObject enemy, Vector2 enemyLoc)
    {
        EnemyTemplateMaster enemyStats = enemy.GetComponent<EnemyTemplateMaster>();
        for (int i = ActiveQuests.Count - 1; i >= 0; i--) //A quest could be completed
        {
            QuestObjective TempActiveObj = ActiveQuests[i].GetComponent<QuestTemplate>().returnActiveObjective();
            if (TempActiveObj.ReturnType() == ObjectiveType.GenericKill)
            {
                ((GenericKillObjective)TempActiveObj).UpdateKillCounts(enemyStats, enemyLoc);
            }
        }
    }
}
