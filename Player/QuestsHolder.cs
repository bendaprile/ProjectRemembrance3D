using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsHolder : MonoBehaviour
{
    private List<GameObject> CompletedQuests = new List<GameObject>();
    private List<GameObject> ActiveQuests = new List<GameObject>();
    private GameObject FocusedQuestReference; //INCLUSIVE WITH ACTIVE QUEST

    private EventQueue eventQueue;
    private PlayerStats playerStats;
    private EventData tempEvent;
    private mapScript Map;


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

    public void QuestSetFocus(GameObject quest_in)
    {
        if (!Map) //If there is not a reference for map already get it (the problem is I cannot get this reference if map is closed)
        {
            GameObject MapTemp = GameObject.Find("MapCanvasHolder");
            if (MapTemp)
            {
                Map = MapTemp.GetComponent<mapScript>();
            }   
        }

        if(quest_in == FocusedQuestReference)
        {
            FocusedQuestReference = null;
        }
        else
        {
            FocusedQuestReference = quest_in;
        }


        if (Map)
        {
            Map.UpdateObjLoc();
        }
    }

    public GameObject ReturnFocus()
    {
        return FocusedQuestReference;
    }

    public void FullQuestCompleted(GameObject UniqueObject)
    {
        int temp_loc = 0;
        foreach(GameObject iter in ActiveQuests)
        {
            if (iter == UniqueObject)
            {
                QuestTemplate QuestiterScript = UniqueObject.GetComponent<QuestTemplate>();
                CompletedQuests.Add(iter);
                ActiveQuests.Remove(iter);
                playerStats.AddEXP(QuestiterScript.xp_reward);

                if(FocusedQuestReference == UniqueObject)
                {
                    FocusedQuestReference = null;
                }

                tempEvent.Setup(EventTypeEnum.QuestCompleted, QuestiterScript.QuestName);
                eventQueue.AddEvent(tempEvent);
                break;
            }
            temp_loc += 1;
        }
    }

    public void CheckFetchObjectives(GameObject item, int location)
    {
        ItemMaster itemStats = item.GetComponent<ItemMaster>();

        for(int i = ActiveQuests.Count - 1; i >= 0; i--) //A quest could be completed
        {
            QuestObjective TempActiveObj = ActiveQuests[i].GetComponent<QuestTemplate>().returnActiveObjective();
            if (TempActiveObj.ReturnType() == ObjectiveType.Fetch)
            {
                ((FetchObjective)TempActiveObj).UpdateItemCount(itemStats, location);
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

    public void CheckExternalObjectiveCompletion(GameObject ObjectiveRef) //Many types of quests
    {
        for (int i = ActiveQuests.Count - 1; i >= 0; i--) //A quest could be completed
        {
            QuestObjective TempActiveObj = ActiveQuests[i].GetComponent<QuestTemplate>().returnActiveObjective();
            TempActiveObj.ExternalObjectiveCompletion(ObjectiveRef);
        }
    }
}
