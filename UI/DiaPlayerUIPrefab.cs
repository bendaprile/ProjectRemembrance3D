using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaPlayerUIPrefab : MonoBehaviour
{

    DiaParent diaParent;
    PlayerStats PS;
    QuestsHolder QH;

    bool pressable;

    DiaPlayerLine TempChildStorage;



    public void Setup(DiaPlayerLine TempChild)
    {
        TempChildStorage = TempChild;
        PS = GameObject.Find("Player").GetComponent<PlayerStats>();
        QH = GameObject.Find("QuestsHolder").GetComponent<QuestsHolder>();

        Text textRef = transform.Find("Text").GetComponent<Text>();

        string viewable_text = "";

        pressable = true;

        if (TempChild.SkillLevelReq > 0)
        {
            int charskill = PS.ReturnNonCombatSkill(TempChild.SkillCheck);
            if (TempChild.SkillLevelReq <= charskill)
            {
                textRef.color = Color.blue;
            }
            else
            {
                textRef.color = Color.red;
                pressable = false;
            }

            viewable_text += "[" + STARTUP_DECLARATIONS.SkillEnumReverse[(int)TempChild.SkillCheck] + " (" + charskill + "/" + TempChild.SkillLevelReq + ")] ";
        }

        if(TempChild.CombatSkillLevelReq > 0)
        {
            int charcskill = PS.ReturnCombatSkill(TempChild.CombatSkillCheck);
            if (TempChild.CombatSkillLevelReq <= PS.ReturnCombatSkill(TempChild.CombatSkillCheck))
            {
                textRef.color = Color.blue;
            }
            else
            {
                textRef.color = Color.red;
                pressable = false;
            }

            viewable_text += "[" + STARTUP_DECLARATIONS.CombatSkillsEnumReverse[(int)TempChild.CombatSkillCheck] + " (" + charcskill + "/" + TempChild.CombatSkillLevelReq + ")] ";
        }

        viewable_text += TempChild.return_line();

        textRef.text = viewable_text;

        diaParent = GameObject.Find("DialogueMenu").GetComponent<DiaParent>();
    }


    public void ButtonPressed()
    {
        if (pressable)
        {
            if(TempChildStorage.return_quest() != null)
            {
                QH.AddQuest(TempChildStorage.return_quest());
            }
                
            diaParent.Continue(TempChildStorage.return_dest());
        }
    }

}
