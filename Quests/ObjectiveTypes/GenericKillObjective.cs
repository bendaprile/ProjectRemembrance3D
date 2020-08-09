using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericKillObjective : QuestObjective
{
    [SerializeField] private List<GameObject> enemyNeeded = new List<GameObject>();
    [SerializeField] private List<int> enemyNumbers = new List<int>();
    [SerializeField] private List<string> Descriptions = new List<string>();

    [SerializeField] private bool LocationRequired = false;
    [SerializeField] private bool LocationHint = false;
    [SerializeField] private List<Vector2> CenterPoint = new List<Vector2>();
    [SerializeField] private List<float> Radius = new List<float>();


    private List<EnemyTemplateMaster> enemyNeededSpecs = new List<EnemyTemplateMaster>();
    private List<int> currentKills = new List<int>();

    public override void initialize()
    {
        base.initialize();
        for (int i = 0; i < enemyNeeded.Count; i++)
        {
            currentKills.Add(0);
            enemyNeededSpecs.Add(enemyNeeded[i].GetComponent<EnemyTemplateMaster>());
        }

    }

    public void UpdateKillCounts(EnemyTemplateMaster etm, Vector2 loc)
    {
        for (int i = 0; i < enemyNeeded.Count; i++)
        {
            if (enemyNeededSpecs[i].EnemyTypeName == etm.EnemyTypeName)
            {
                bool conditionsMet = false;
                if (LocationRequired)
                {
                    float dist = Mathf.Sqrt(Mathf.Pow(CenterPoint[i].x - loc.x, 2f) + Mathf.Pow(CenterPoint[i].y - loc.y, 2f));
                    if(dist < Radius[i])
                    {
                        conditionsMet = true;
                    }
                }
                else
                {
                    conditionsMet = true;
                }

                if(conditionsMet)
                {
                    currentKills[i] += 1;
                }
            }
        }

        CheckStatus();
    }

    public override List<(bool, string)> ReturnTasks()
    {
        List<(bool, string)> taskList = new List<(bool, string)>();

        for (int i = 0; i < enemyNeeded.Count; i++)
        {
            string amount_completed = " (" + currentKills[i] + "/" + enemyNumbers[i] + ")";
            taskList.Add(((currentKills[i] >= enemyNumbers[i]), (Descriptions[i] + amount_completed)));
        }

        return taskList;
    }

    public override ObjectiveType ReturnType()
    {
        return ObjectiveType.GenericKill;
    }

    protected override void CheckStatus()
    {
        bool objFinished = true;
        for (int i = 0; i < enemyNeeded.Count; i++)
        {
            if (currentKills[i] < enemyNumbers[i])
            {
                objFinished = false;
            }
        }

        if (objFinished)
        {
            questTemplate.ObjectiveFinished();
        }
    }
}
