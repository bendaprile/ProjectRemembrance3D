using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaParent : MonoBehaviour
{
    List<Transform> npcLines = new List<Transform>();
    private Text npcText;

    [SerializeField] private GameObject PlayerLinePrefab;
    [SerializeField] private UIController uIcontroller;
    [SerializeField] private GameObject NpcTextPanel;
    [SerializeField] private GameObject ContinueButton; 
    [SerializeField] private Transform DiaPlayerPanel;

    bool first_setup = true;
    string next_dest; //used when there is no player input


    public void SetupDia(Transform DiaData, string startingLine)
    {
        if (first_setup)
        {
            FirstSetup();
        }

        foreach(Transform child in DiaData)
        {
            npcLines.Add(child);
        }
        next_dest = ""; //used when there is no player input
        ViewText(startingLine);
    }


    public void Continue(string dest)
    {
        if(dest != "") //Player input
        {
            ViewText(dest);
        }
        else if(next_dest != "") //next line
        {
            ViewText(next_dest);
        }
        else
        {
            uIcontroller.DialogueMenuBool();
        }
    }


    private void ViewText(string iden)
    {
        foreach(Transform child in DiaPlayerPanel) //clean player panel
        {
            Destroy(child.gameObject);
        }

        Transform selectedLine = null;
        foreach(Transform line in npcLines) //find next line
        {
            if(line.name == iden)
            {
                selectedLine = line;
            }
        }

        if(!selectedLine)
        {
            Debug.Log("Dia destination name not found");
        }
        else
        {
            npcText.text = selectedLine.GetComponent<DiaNpcLine>().Line;
            next_dest = selectedLine.GetComponent<DiaNpcLine>().Dest;

            bool is_playerLines = SetupPlayerLines(selectedLine);

            if (is_playerLines)
            {
                ContinueButton.SetActive(false);
                DiaPlayerPanel.gameObject.SetActive(true);
            }
            else
            {
                ContinueButton.SetActive(true);
                DiaPlayerPanel.gameObject.SetActive(false);
            }


        }
    }

    private bool SetupPlayerLines(Transform selectedLine)
    {
        bool is_playerLines = false;
        foreach (Transform child in selectedLine)
        {
            DiaPlayerLine TempChild = child.GetComponent<DiaPlayerLine>();
            is_playerLines = true;
            GameObject temp = Instantiate(PlayerLinePrefab, DiaPlayerPanel);
            temp.GetComponent<DiaPlayerUIPrefab>().Setup(TempChild);
        }

        return is_playerLines;
    }

    private void OnDisable()
    {
        npcLines.Clear();
    }

    private void FirstSetup()
    {
        npcText = NpcTextPanel.GetComponent<Text>();
        first_setup = false;
    }
}
