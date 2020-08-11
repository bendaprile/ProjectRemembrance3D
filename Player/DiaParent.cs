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
    [SerializeField] private Transform DiaNoOptions;

    bool first_setup = true;
    Transform next_dest; //used when there is no player input
    private DiaRoot diaRoot;


    public void SetupDia(Transform DiaRoot)
    {
        if (first_setup)
        {
            FirstSetup();
        }

        diaRoot = DiaRoot.GetComponent<DiaRoot>();

        foreach (Transform child in DiaRoot)
        {
            npcLines.Add(child);
        }
        next_dest = null; //used when there is no player input
        ViewText(diaRoot.ReturnStarting());
    }


    public void Continue(Transform dest)
    {
        if(dest == DiaNoOptions) //next line // this object is a child of the continue button, but it doesnt matter where it is. Cannot a button cannot send null, so I have this random transform
        {
            ViewText(next_dest);
        }
        else if(dest != null) //next line
        {
            ViewText(dest);
        }
        else
        {
            ResetNpcLines();
            uIcontroller.DialogueMenuBool();
        }
    }


    private void ViewText(Transform iden)
    {
        foreach(Transform child in DiaPlayerPanel) //clean player panel
        {
            Destroy(child.gameObject);
        }

        Transform selectedLine = null;
        foreach(Transform line in npcLines) //find next line
        {
            if(line == iden)
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
            npcText.text = selectedLine.GetComponent<DiaNpcLine>().return_line();
            next_dest = selectedLine.GetComponent<DiaNpcLine>().return_dest();

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
            if (child.gameObject.activeSelf)
            {
                DiaPlayerLine TempChild = child.GetComponent<DiaPlayerLine>();
                is_playerLines = true;
                GameObject temp = Instantiate(PlayerLinePrefab, DiaPlayerPanel);
                temp.GetComponent<DiaPlayerUIPrefab>().Setup(TempChild);
            }
        }

        return is_playerLines;
    }

    private void ResetNpcLines()
    {
        foreach(Transform npcLine in npcLines)
        {
            npcLine.GetComponent<DiaNpcLine>().Reset();
        }
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
