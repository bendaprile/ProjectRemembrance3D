using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ReturnFetchObject : QuestObjective
{
    [SerializeField] private GameObject ConnectedFetchQuest;
    [SerializeField] private GameObject EnableDia;
    [SerializeField] private string Description;
    [SerializeField] private Transform ReturnLocation;
    private Inventory inventory;

    private List<int> itemLocs;

    public override void initialize()
    {
        Assert.AreNotEqual(ReturnLocation, null);
        base.initialize();
        itemLocs = ConnectedFetchQuest.GetComponent<FetchObjective>().ReturnItemLocs();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        EnableDia.GetComponent<DiaPlayerLine>().mark_dia_for_quest(gameObject);
    }

    public override void ExternalObjectiveCompletion(GameObject ObjectiveRef)
    {
        if(ObjectiveRef == gameObject)
        {
            foreach(int item in itemLocs)
            {
                inventory.DeleteItem(item);
            }
            questTemplate.ObjectiveFinished();
        }
    }

    public override List<(bool, string)> ReturnTasks()
    {
        List<(bool, string)> taskList = new List<(bool, string)>();
        taskList.Add((false, Description));
        return taskList;
    }

    public override (bool, List<(Vector2, float)>) ReturnLocs()
    {
        List<(Vector2, float)> tempList = new List<(Vector2, float)>();
        tempList.Add((new Vector2(ReturnLocation.position.x, ReturnLocation.position.z), 10f));
        return (false, tempList);
    }
}
