using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GenericKillObjective : QuestObjective
{
    [SerializeField] private List<GameObject> enemyNeeded = new List<GameObject>();
    [SerializeField] private List<int> enemyNumbers = new List<int>();
    [SerializeField] private List<string> Descriptions = new List<string>();

    [SerializeField] private bool LocationRequired = false;

    private List<EnemyTemplateMaster> enemyNeededSpecs = new List<EnemyTemplateMaster>();
    private List<int> currentKills = new List<int>();

    public override void initialize()
    {
        Assert.AreEqual(NumberOfTasks, enemyNeeded.Count);
        Assert.AreEqual(enemyNeeded.Count, enemyNumbers.Count);
        Assert.AreEqual(enemyNumbers.Count, Descriptions.Count);

        base.initialize();
        for (int i = 0; i < NumberOfTasks; i++)
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
            taskList.Add(((TaskComplete(i)), (Descriptions[i] + amount_completed)));
        }

        return taskList;
    }

    protected override bool TaskComplete(int i)
    {
        return currentKills[i] >= enemyNumbers[i];
    }

    public override ObjectiveType ReturnType()
    {
        return ObjectiveType.GenericKill;
    }

    public override (bool, List<(Vector2, float)>) ReturnLocs()
    {
        if (LocationHint || LocationRequired)
        {
            return (LocationRequired, ReturnLocsHelper());
        }
        else
        {
            return (false, new List<(Vector2, float)>());
        }
    }
}
