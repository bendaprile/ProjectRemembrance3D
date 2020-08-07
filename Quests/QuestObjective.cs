using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    protected QuestTemplate questTemplate;

    public virtual void initialize()
    {
        questTemplate = GetComponentInParent<QuestTemplate>();
    }

    public virtual List<(bool, string)> ReturnTasks()
    {
        return new List<(bool, string)>();
    }

    public virtual ObjectiveType ReturnType()
    {
        return ObjectiveType.NOTHING; 
    }

    protected virtual void CheckStatus()
    {

    }
}
