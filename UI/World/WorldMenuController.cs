using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMenuController : MonoBehaviour
{
    [SerializeField] private Transform mapRect;
    [SerializeField] private Transform QuestViewInfo;
    [SerializeField] private Transform DetailViewInfo;

    [SerializeField] private RectTransform QuestUIPrefab;
    [SerializeField] private RectTransform QuestDetailUIPrefab;
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

        Transform tempPrefab;
        tempPrefab = Instantiate(QuestDetailUIPrefab, DetailViewInfo);
        QuestObjective tempActiveQuest = tempList[iter_in].GetComponent<QuestTemplate>().returnActiveObjective();
        tempPrefab.GetComponentInChildren<Text>().text = tempActiveQuest.ReturnDescription();

        Instantiate(SpacerUIPrefab, DetailViewInfo);

        List<QuestObjective> tempObjList = tempList[iter_in].GetComponent<QuestTemplate>().returnCompletedObjectives();

        for(int i = tempObjList.Count - 1; i >= 0; i--)
        {
            tempPrefab = Instantiate(QuestDetailUIPrefab, DetailViewInfo);
            tempPrefab.GetComponentInChildren<Text>().text = tempObjList[i].ReturnDescription();
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
