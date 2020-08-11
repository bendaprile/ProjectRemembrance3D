using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class QuestObjective : MonoBehaviour
{
    [SerializeField] protected int NumberOfTasks = 1;
    [SerializeField] protected bool LocationHint = false;
    [SerializeField] protected List<Vector2> CenterPoint = new List<Vector2>();
    [SerializeField] protected List<float> Radius = new List<float>();

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

    public virtual (bool, List<(Vector2, float)>) ReturnLocs()
    {
        if (LocationHint)
        {
            return (false, ReturnLocsHelper());
        }
        else
        {
            return (false, new List<(Vector2, float)>());
        }
    }

    public virtual void ExternalObjectiveCompletion(GameObject ObjectiveRef)
    {

    }

    protected virtual bool TaskComplete(int i)
    {
        return false;
    }

    //FIXED FUNCTIONS
    protected void CheckStatus()
    {
        bool objFinished = true;
        for (int i = 0; i < NumberOfTasks; i++)
        {
            if (!TaskComplete(i))
            {
                objFinished = false;
            }
        }

        if (objFinished)
        {
            questTemplate.ObjectiveFinished();
        }
    }

    protected List<(Vector2, float)> ReturnLocsHelper()
    {
        List<(Vector2, float)> TempList = new List<(Vector2, float)>();
        for (int i = 0; i < CenterPoint.Count; i++)
        {
            if (!TaskComplete(i))
            {
                TempList.Add((CenterPoint[i], Radius[i]));
            }
        }
        return TempList;
    }
    ////////////////////
}
