using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaPlayerLine : MonoBehaviour
{
    [SerializeField] public string Line;
    [SerializeField] public string Dest;

    [SerializeField] public Transform Quest; //null if no quest

    [SerializeField] public int SkillLevelReq; //0 if no check
    [SerializeField] public SkillsEnum SkillCheck;

    [SerializeField] public int CombatSkillLevelReq; //0 if no check
    [SerializeField] public CombatSkillsEnum CombatSkillCheck;

    
}
