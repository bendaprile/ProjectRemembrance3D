using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNpcLine : MonoBehaviour
{
    [SerializeField] private List<string> Lines = new List<string>();

    [SerializeField] private bool cycleLines;

    [SerializeField] private bool cycleLinesNoWrapAround;
    [SerializeField] private bool resetCycle;

    [SerializeField] private Transform Dest;

    private int currentLine = 0;

    public string return_line()
    {
        if (cycleLines)
        {
            string templine = Lines[currentLine];
            currentLine = (currentLine + 1) % Lines.Count;
            return templine;
        }
        else if (cycleLinesNoWrapAround)
        {
            string templine = Lines[currentLine];
            if(currentLine < (Lines.Count - 1))
            {
                currentLine += 1;
            }
            return templine;
        }
        else if(Lines.Count > 0)
        {
            return Lines[0];
        }
        else
        {
            return "";
        }
    }

    public Transform return_dest()
    {
        return Dest;
    }

    public void Reset()
    {
        if (resetCycle)
        {
            currentLine = 0;
        }
    }
}
