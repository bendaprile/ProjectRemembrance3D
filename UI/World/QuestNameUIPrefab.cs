using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNameUIPrefab : MonoBehaviour
{
    int iterStorage;

    public void Setup(GameObject quest, int iter)
    {
        QuestTemplate questScript = quest.GetComponent<QuestTemplate>();
        iterStorage = iter;
        gameObject.GetComponentInChildren<Text>().text = questScript.QuestName;
    }

    public void ButtonPressed()
    {
        GameObject WorldMenu = GameObject.Find("WorldMenu");
        WorldMenu.GetComponent<WorldMenuController>().GetDetails(iterStorage);
    }
}
