using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    [SerializeField] protected string Description;
    protected QuestTemplate questTemplate;

    public virtual void initialize()
    {
        questTemplate = GetComponentInParent<QuestTemplate>();
    }

    public virtual string ReturnDescription()
    {
        return "";
    }

    public virtual ObjectiveType ReturnType()
    {
        return ObjectiveType.NOTHING; 
    }

    protected virtual void CheckStatus()
    {

    }
}
