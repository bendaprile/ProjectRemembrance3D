using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTemplate : MonoBehaviour
{
    [SerializeField] public string QuestName; //MUST BE UNIQUE
    [SerializeField] public int xp_reward;

    private List<GameObject> HiddenObjectiveList = new List<GameObject>();
    private GameObject ActiveObjective;
    private List<GameObject> CompletedObjectiveList = new List<GameObject>();

    private EventQueue eventQueue;
    private QuestsHolder questsHolder;

    public QuestObjective returnActiveObjective()
    {
        return ActiveObjective.GetComponent<QuestObjective>();
    }

    public List<QuestObjective> returnCompletedObjectives()
    {
        List<QuestObjective> temp = new List<QuestObjective>();
        foreach(GameObject iter in CompletedObjectiveList)
        {
            temp.Add(iter.GetComponent<QuestObjective>());
        }
        return temp;
    }

    private void Awake()
    {
        eventQueue = GameObject.Find("EventDisplay").GetComponent<EventQueue>();
        questsHolder = GameObject.Find("QuestsHolder").GetComponent<QuestsHolder>();
    }

    public void QuestStart()
    {
        bool first = true;
        foreach(Transform child in transform)
        {
            if (first)
            {
                ActiveObjective = child.gameObject;
                ActiveObjective.GetComponent<QuestObjective>().initialize();
                first = false;
            }
            else
            {
                HiddenObjectiveList.Add(child.gameObject);
            }
        }
    }

    public void ObjectiveFinished()
    {
        CompletedObjectiveList.Add(ActiveObjective);

        /////
        EventData tempEvent = new EventData();
        tempEvent.Setup(EventTypeEnum.QuestObjCompleted, GetComponentInParent<QuestTemplate>().QuestName);
        eventQueue.AddEvent(tempEvent);
        /////

        ActiveObjective = null;
        if (HiddenObjectiveList.Count > 0)
        {
            ActiveObjective = HiddenObjectiveList[0];
            HiddenObjectiveList.RemoveAt(0);
            ActiveObjective.GetComponent<QuestObjective>().initialize();
        }
        else
        {
            questsHolder.FullQuestCompleted(QuestName);
        }


    }
}
