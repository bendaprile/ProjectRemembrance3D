using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaPlayerLine : MonoBehaviour
{
    [SerializeField] private string Line;
    [SerializeField] private Transform Dest;

    [SerializeField] private Transform Quest = null; //null if no quest

    [SerializeField] public int SkillLevelReq = 0; //0 if no check
    [SerializeField] public SkillsEnum SkillCheck;

    [SerializeField] public int CombatSkillLevelReq = 0; //0 if no check
    [SerializeField] public CombatSkillsEnum CombatSkillCheck;

    [SerializeField] public List<GameObject> DisableList; //If used for Dia can only disable player options

    public string return_line()
    {
        return Line;
    }

    public Transform return_dest() //This means that it is clicked
    {
        foreach(GameObject temp in DisableList) 
        {
            temp.SetActive(false);
        }
        return Dest;
    }

    public Transform return_quest()
    {
        return Quest;
    }
}
