using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuController : MonoBehaviour
{
    private PlayerStats playerStats;

    private GameObject Strength;
    private GameObject Dexterity;
    private GameObject Charisma;
    private GameObject Intelligence;
    private GameObject Toughness;
    private GameObject Willpower;

    private GameObject Vigor;
    private GameObject Cerebral;
    private GameObject Fortitude;

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

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        Strength = transform.Find("IntrinsicSkillLayout").Find("StrengthPanel").Find("StrengthVar").gameObject;
        Dexterity = transform.Find("IntrinsicSkillLayout").Find("DexterityPanel").Find("DexterityVar").gameObject;
        Charisma = transform.Find("IntrinsicSkillLayout").Find("CharismaPanel").Find("CharismaVar").gameObject;
        Intelligence = transform.Find("IntrinsicSkillLayout").Find("IntelligencePanel").Find("IntelligenceVar").gameObject;
        Toughness = transform.Find("IntrinsicSkillLayout").Find("ToughnessPanel").Find("ToughnessVar").gameObject;
        Willpower = transform.Find("IntrinsicSkillLayout").Find("WillpowerPanel").Find("WillpowerVar").gameObject;
        Vigor = transform.Find("IntrinsicSkillLayout").Find("VigorPanel").Find("VigorVar").gameObject;
        Cerebral = transform.Find("IntrinsicSkillLayout").Find("CerebralPanel").Find("CerebralVar").gameObject;
        Fortitude = transform.Find("IntrinsicSkillLayout").Find("FortitudePanel").Find("FortitudeVar").gameObject;

        Larceny = transform.Find("GeneralSkillsLayout").Find("LarcenyPanel").Find("Var").gameObject;
        Science = transform.Find("GeneralSkillsLayout").Find("SciencePanel").Find("Var").gameObject;
        Medicine = transform.Find("GeneralSkillsLayout").Find("MedicinePanel").Find("Var").gameObject;
        Repair = transform.Find("GeneralSkillsLayout").Find("RepairPanel").Find("Var").gameObject;
        Speech = transform.Find("GeneralSkillsLayout").Find("SpeechPanel").Find("Var").gameObject;
        Survival = transform.Find("GeneralSkillsLayout").Find("SurvivalPanel").Find("Var").gameObject;
        Perception = transform.Find("GeneralSkillsLayout").Find("PerceptionPanel").Find("Var").gameObject;

        Ranged = transform.Find("CombatSkillsLayout").Find("RangedPanel").Find("Var").gameObject;
        Melee = transform.Find("CombatSkillsLayout").Find("MeleePanel").Find("Var").gameObject;
        Armor = transform.Find("CombatSkillsLayout").Find("ArmorPanel").Find("Var").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Strength.GetComponent<Text>().text =        (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Strength)).ToString();
        Dexterity.GetComponent<Text>().text =       (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Dexterity)).ToString();
        Charisma.GetComponent<Text>().text =        (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Charisma)).ToString();
        Intelligence.GetComponent<Text>().text =    (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Intelligence)).ToString();
        Toughness.GetComponent<Text>().text =       (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Toughness)).ToString();
        Willpower.GetComponent<Text>().text =       (playerStats.ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Willpower)).ToString();

        Vigor.GetComponent<Text>().text =           (playerStats.ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Vigor)).ToString();
        Cerebral.GetComponent<Text>().text =        (playerStats.ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Cerebral)).ToString();
        Fortitude.GetComponent<Text>().text =       (playerStats.ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Fortitude)).ToString();

        Larceny.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Larceny)).ToString();
        Science.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Science)).ToString();
        Medicine.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Medicine)).ToString();
        Repair.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Repair)).ToString();
        Speech.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Speech)).ToString();
        Survival.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Survival)).ToString();
        Perception.GetComponent<Text>().text = (playerStats.ReturnNonCombatSkill(SkillsEnum.Perception)).ToString();

        Ranged.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.ranged_proficiency)).ToString();
        Melee.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.melee_proficiency)).ToString();
        Armor.GetComponent<Text>().text = (playerStats.ReturnCombatSkill(CombatSkillsEnum.armor_proficiency)).ToString();
    }
}
