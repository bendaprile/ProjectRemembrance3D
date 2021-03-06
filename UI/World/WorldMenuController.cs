﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuController : MonoBehaviour
{
    [SerializeField] private Transform mapRect;
    [SerializeField] private Transform QuestViewInfo;
    [SerializeField] private Transform DetailViewInfo;

    [SerializeField] private RectTransform QuestUIPrefab;
    [SerializeField] private RectTransform CQDtext;
    [SerializeField] private RectTransform ComplextQuestDetailUIPrefab;
    [SerializeField] private RectTransform SpacerUIPrefab;

    private bool First_run = true;
    private List<GameObject> tempList;
    private QuestsHolder QuestsHolder;


    public void GetDetails(int iter_in)
    {
        foreach (Transform iter in DetailViewInfo)
        {
            Destroy(iter.gameObject);
        }
        
        QuestObjective tempActiveQuest = tempList[iter_in].GetComponent<QuestTemplate>().returnActiveObjective();
        SetupComplex(tempActiveQuest);

        Instantiate(SpacerUIPrefab, DetailViewInfo);

        List<QuestObjective> tempObjList = tempList[iter_in].GetComponent<QuestTemplate>().returnCompletedObjectives();

        for(int i = tempObjList.Count - 1; i >= 0; i--)
        {
            SetupComplex(tempObjList[i]);   
        }
    }

    public void QuestSetFocus(int iter_in)
    {
        QuestsHolder.QuestSetFocus(tempList[iter_in]);
        UpdateFocusUI(QuestsHolder.ReturnFocus());
    }

    private void SetupComplex(QuestObjective tempObj)
    {
        RectTransform tempPrefab = Instantiate(ComplextQuestDetailUIPrefab, DetailViewInfo);

        List<(bool, string)> taskList = tempObj.ReturnTasks();

        tempPrefab.sizeDelta = new Vector2(tempPrefab.sizeDelta.x, 0);
        foreach ((bool, string) task in taskList)
        {
            Transform textPrefab = Instantiate(CQDtext, tempPrefab);

            if(task.Item1)
            {
                textPrefab.GetComponent<Text>().color = Color.gray;
            }
            else
            {
                textPrefab.GetComponent<Text>().color = Color.black;
            }

            textPrefab.GetComponent<Text>().text = task.Item2;
            tempPrefab.sizeDelta = new Vector2(tempPrefab.sizeDelta.x, tempPrefab.sizeDelta.y + 50);
        }
    }


    private void OnEnable()
    {
        if (First_run)
        {
            first_run_func();
        }
        SetupQuests();
        mapRect.localPosition = new Vector3(410f, 0f);
    }

    private void SetupQuests()
    {
        tempList = QuestsHolder.ReturnActiveQuests();

        int iter = 0;
        foreach(GameObject quest in tempList)
        {
            Transform tempPrefab = Instantiate(QuestUIPrefab, QuestViewInfo);
            tempPrefab.GetComponent<QuestNameUIPrefab>().Setup(quest, iter);
            iter += 1;
        }

        UpdateFocusUI(QuestsHolder.ReturnFocus());
    }

    private void UpdateFocusUI(GameObject temp)
    {
        foreach(Transform child in QuestViewInfo)
        {
            child.GetComponent<QuestNameUIPrefab>().CheckFocus(temp);
        }
    }

    private void first_run_func()
    {
        QuestsHolder = GameObject.Find("QuestsHolder").GetComponent<QuestsHolder>();
        First_run = false;
    }

    private void OnDisable()
    {
        mapRect.localPosition = new Vector3(0f, 0f);

        foreach (Transform iter in QuestViewInfo)
        {
            Destroy(iter.gameObject);
        }

        foreach (Transform iter in DetailViewInfo)
        {
            Destroy(iter.gameObject);
        }
    }
}
