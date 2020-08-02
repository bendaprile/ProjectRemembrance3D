using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    private GameObject FirstPanel;
    private GameObject GeneralSkillsLayout;
    private GameObject CombatSkillsLayout;
    private PlayerStats playerStats;
    private UIController uIController;


    private GameObject Larceny;
    private GameObject Science;
    private GameObject Medicine;
    private GameObject Repair;
    private GameObject Speech;
    private GameObject Survival;
    private GameObject Perception;

    private GameObject Ranged;
    private GameObject Melee;
    private GameObject Armor;

    private GameObject Finalize;

    private Text GeneralPointsText;
    private Text CombatPointsText;


    private int[] temp_generalSkills = new int[7];
    private int[] temp_combatSkills = new int[3];

    private int FreeGeneralSkillPoints;
    private int FreeCombatSkillPoints;


    private (SkillsEnum, string)[] skills_conv = new (SkillsEnum, string)[7];
    private (CombatSkillsEnum, string)[] combat_skills_conv = new (CombatSkillsEnum, string)[3];

    private bool first_enable = true;

    void first_enable_func()
    {
        FirstPanel = transform.Find("FirstPanel").gameObject;
        GeneralSkillsLayout = FirstPanel.transform.Find("GeneralSkillsLayout").gameObject;
        CombatSkillsLayout = FirstPanel.transform.Find("CombatSkillsLayout").gameObject;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        uIController = GetComponentInParent<UIController>();
        setupSkillsConvs();

        Larceny = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("LarcenyPanel").Find("Var").gameObject;
        Science = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("SciencePanel").Find("Var").gameObject;
        Medicine = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("MedicinePanel").Find("Var").gameObject;
        Repair = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("RepairPanel").Find("Var").gameObject;
        Speech = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("SpeechPanel").Find("Var").gameObject;
        Survival = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("SurvivalPanel").Find("Var").gameObject;
        Perception = transform.Find("FirstPanel").Find("GeneralSkillsLayout").Find("PerceptionPanel").Find("Var").gameObject;

        Ranged = transform.Find("FirstPanel").Find("CombatSkillsLayout").Find("RangedPanel").Find("Var").gameObject;
        Melee = transform.Find("FirstPanel").Find("CombatSkillsLayout").Find("MeleePanel").Find("Var").gameObject;
        Armor = transform.Find("FirstPanel").Find("CombatSkillsLayout").Find("ArmorPanel").Find("Var").gameObject;

        Finalize = transform.Find("FirstPanel").Find("Finalize").gameObject;

        GeneralPointsText = transform.Find("FirstPanel").Find("GeneralSkills").Find("Points").GetComponent<Text>();
        CombatPointsText = transform.Find("FirstPanel").Find("CombatSkills").Find("Points").GetComponent<Text>();
    }


    private void OnEnable()
    {
        if (first_enable)
        {
            first_enable = false;
            first_enable_func();
        }
        for(int i = 0; i < 7; i++)
        {
            temp_generalSkills[i] = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            temp_combatSkills[i] = 0;
        }

        Finalize.SetActive(false);
        FreeGeneralSkillPoints = playerStats.returnFreeSkillPoints();
        FreeCombatSkillPoints = playerStats.returnFreeSkillPoints();
    }


    private void setupSkillsConvs()
    {
        skills_conv[0] = (SkillsEnum.Larceny, "Larceny");
        skills_conv[1] = (SkillsEnum.Science, "Science");
        skills_conv[2] = (SkillsEnum.Medicine, "Medicine");
        skills_conv[3] = (SkillsEnum.Repair, "Repair");
        skills_conv[4] = (SkillsEnum.Speech, "Speech");
        skills_conv[5] = (SkillsEnum.Survival, "Survival");
        skills_conv[6] = (SkillsEnum.Perception, "Perception");

        combat_skills_conv[0] = (CombatSkillsEnum.ranged_proficiency, "ranged_proficiency");
        combat_skills_conv[1] = (CombatSkillsEnum.melee_proficiency, "melee_proficiency");
        combat_skills_conv[2] = (CombatSkillsEnum.armor_proficiency, "armor_proficiency");
    }


    public void IncreaseGeneralSkill(int skill)
    {
        if(FreeGeneralSkillPoints > 0)
        {
            temp_generalSkills[skill] += 1;
            FreeGeneralSkillPoints -= 1;
        }
    }

    public void DecreaseGeneralSkill(int skill)
    {
        if(temp_generalSkills[skill] > 0)
        {
            temp_generalSkills[skill] -= 1;
            FreeGeneralSkillPoints += 1;
        }
    }

    public void IncreaseCombatSkill(int skill)
    {
        if(FreeCombatSkillPoints > 0)
        {
            temp_combatSkills[skill] += 1;
            FreeCombatSkillPoints -= 1;
        }
    }

    public void DecreaseCombatSkill(int skill)
    {
        if (temp_combatSkills[skill] > 0)
        {
            temp_combatSkills[skill] -= 1;
            FreeCombatSkillPoints += 1;
        }
    }

    public void FinalizeFunc()
    {
        uIController.LevelUpMenuBool(false);
    }

    // Update is called once per frame
    void Update()
    {
        Larceny.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Larceny) + temp_generalSkills[0]).ToString();
        Science.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Science) + temp_generalSkills[1]).ToString();
        Medicine.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Medicine) + temp_generalSkills[2]).ToString();
        Repair.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Repair) + temp_generalSkills[3]).ToString();
        Speech.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Speech) + temp_generalSkills[4]).ToString();
        Survival.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Survival) + temp_generalSkills[5]).ToString();
        Perception.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Perception) + temp_generalSkills[6]).ToString();

        Ranged.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.ranged_proficiency) + temp_combatSkills[0]).ToString();
        Melee.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.melee_proficiency) + temp_combatSkills[1]).ToString();
        Armor.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.armor_proficiency) + temp_combatSkills[2]).ToString();

        GeneralPointsText.text = FreeGeneralSkillPoints.ToString();
        CombatPointsText.text = FreeCombatSkillPoints.ToString();

        if(FreeGeneralSkillPoints + FreeCombatSkillPoints == 0)
        {
            Finalize.SetActive(true);
        }
    }
}
