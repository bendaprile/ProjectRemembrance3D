using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNameUIPrefab : MonoBehaviour
{
    [SerializeField] private Transform Details;
    [SerializeField] private Transform SetActive;

    private GameObject QuestRef;
    private int iterStorage;
    private WorldMenuController WorldMenu;

    public void Setup(GameObject quest, int iter)
    {
        QuestRef = quest;
        QuestTemplate questScript = quest.GetComponent<QuestTemplate>();
        iterStorage = iter;
        Details.GetComponentInChildren<Text>().text = questScript.QuestName;
        WorldMenu = GameObject.Find("WorldMenu").GetComponent<WorldMenuController>();
    }

    public void CheckFocus(GameObject temp)
    {
        if(temp == QuestRef)
        {
            transform.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }

    public void DetailButtonPressed()
    {
        WorldMenu.GetDetails(iterStorage);
    }

    public void ActiveButtonPressed()
    {
        WorldMenu.QuestSetFocus(iterStorage);
    }
}
