using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    private GameObject Player;
    private Health PlayerHealth;
    private Energy PlayerEnergy;
    private CombatChecker combatChecker;
    private UIController uIController;
    private EventQueue eventQueue;

    //////////////////////////////////////
    private int level;
    private int current_exp;
    private bool LevelUpQued = false;

    public void AddEXP(int exp_in)
    {
        current_exp += exp_in;
    }

    public int returnEXP()
    {
        return (current_exp);
    }

    public int returnLevel()
    {
        return level;
    }

    public float returnEXPToNextLevel(int exp_in)
    {
        return (current_exp / (level * 1000));
    }

    public int returnFreeSkillPoints()
    {
        return (5 + ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Cerebral) / 4);
    }

    private void LevelUp()
    {
        if (current_exp > level * 1000)
        {
            if (!LevelUpQued)
            {
                LevelUpQued = true;

                /////
                EventData tempEvent = new EventData();
                tempEvent.Setup(EventTypeEnum.LevelUp, "");
                eventQueue.AddEvent(tempEvent);
                /////
            }

            if (!combatChecker.enemies_nearby)
            {
                current_exp -= level * 1000;
                level += 1;
                UpdateBaseAttributes();
                uIController.LevelUpMenuBool(true);
                LevelUpQued = false;
            }
        }
    }

    //////////////////////////////////////



    //////////////////////////////////////
    private int[] CoreStatsStorage = new int[6];
    private int[] DerivedStatsStorage = new int[3];

    public void ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum CoreIntrinsicSkill, int value)
    {
        CoreStatsStorage[(int)CoreIntrinsicSkill] = value;
        UpdateBaseAttributes();
        UpdateDerivedStats();
    }

    public int ReturnCoreIntrinsicSkill(CoreIntrinsicSkillsEnum CoreIntrinsicSkill)
    {
        return CoreStatsStorage[(int)CoreIntrinsicSkill];
    }

    public int ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum DerivedIntrinsicSkill)
    {
        return DerivedStatsStorage[(int)DerivedIntrinsicSkill];
    }

    private void UpdateDerivedStats()
    {
        DerivedStatsStorage[(int)DerivedIntrinsicSkillsEnum.Vigor] = CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Strength] + CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Dexterity];
        DerivedStatsStorage[(int)DerivedIntrinsicSkillsEnum.Cerebral] = CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Charisma] + CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Intelligence];
        DerivedStatsStorage[(int)DerivedIntrinsicSkillsEnum.Fortitude] = CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Toughness] + CoreStatsStorage[(int)CoreIntrinsicSkillsEnum.Willpower];
    }
    //////////////////////////////////////



    //////////////////////////////////////
    private float[] FinalAttributesStorage = new float[STARTUP_DECLARATIONS.Number_of_Attributes];
    public List<(string, float)>[] AttributesAdditiveEffects = new List<(string, float)>[STARTUP_DECLARATIONS.Number_of_Attributes];
    public List<(string, float)>[] AttributesMultiplicativeEffects = new List<(string, float)>[STARTUP_DECLARATIONS.Number_of_Attributes];

    public void UpdateBaseAttributes()
    {
        AddAttributeEffect(AttributesEnum.max_health, "Base", true, 50);
        AddAttributeEffect(AttributesEnum.max_health, "Fortitude", true, level * ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Fortitude));
        AddAttributeEffect(AttributesEnum.health_regen, "Base", true, .1f);
        AddAttributeEffect(AttributesEnum.max_energy, "Base", true, 100);
        AddAttributeEffect(AttributesEnum.max_energy, "Vigor", true, level * ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Vigor) / 2);
        AddAttributeEffect(AttributesEnum.max_expanded_energy, "Base", true, 100);
        AddAttributeEffect(AttributesEnum.max_expanded_energy, "Vigor", true, level * ReturnDerivedIntrinsicSkill(DerivedIntrinsicSkillsEnum.Vigor) / 2);
        AddAttributeEffect(AttributesEnum.energy_regen, "Base", true, .3f);
        AddAttributeEffect(AttributesEnum.expanded_energy_regen, "Base", true, .1f);
        AddAttributeEffect(AttributesEnum.armor, "Armor Proficiency", true, ReturnCombatSkill(CombatSkillsEnum.armor_proficiency));
    }

    public float ReturnAttribute(AttributesEnum AttributeName)
    {
        return FinalAttributesStorage[(int)AttributeName];
    }

    public void AddAttributeEffect(AttributesEnum AttributeName, string EffectName, bool isAdd, float value) //Overwrites effect with the same name
    {
        if (isAdd)
        {
            for(int location = 0; location < AttributesAdditiveEffects[(int)AttributeName].Count; ++location)
            {
                if(AttributesAdditiveEffects[(int)AttributeName][location].Item1 == EffectName)
                {
                    AttributesAdditiveEffects[(int)AttributeName][location] = (EffectName, value);
                    RecalculateAttribute((int)AttributeName);
                    return;
                }
            }
            AttributesAdditiveEffects[(int)AttributeName].Add((EffectName, value));
        }
        else
        {
            for (int location = 0; location < AttributesMultiplicativeEffects[(int)AttributeName].Count; ++location)
            {
                if (AttributesMultiplicativeEffects[(int)AttributeName][location].Item1 == EffectName)
                {
                    AttributesMultiplicativeEffects[(int)AttributeName][location] = (EffectName, value);
                    RecalculateAttribute((int)AttributeName);
                    return;
                }
            }
            AttributesMultiplicativeEffects[(int)AttributeName].Add((EffectName, value));
        }
        RecalculateAttribute((int)AttributeName);
    }

    public void RemoveAttributeEffect(AttributesEnum AttributeName, string EffectName, bool isAdd)
    {
        if (isAdd)
        {
            int len = AttributesAdditiveEffects[(int)AttributeName].Count;
            for (int i = 0; i < len; i++)
            {
                if (AttributesAdditiveEffects[(int)AttributeName][i].Item1 == EffectName)
                {
                    AttributesAdditiveEffects[(int)AttributeName].RemoveAt(i);
                    break;
                }
            }
        }
        else
        {
            int len = AttributesMultiplicativeEffects[(int)AttributeName].Count;
            for (int i = 0; i < len; i++)
            {
                if (AttributesMultiplicativeEffects[(int)AttributeName][i].Item1 == EffectName)
                {
                    AttributesMultiplicativeEffects[(int)AttributeName].RemoveAt(i);
                    break;
                }
            }
        }
        RecalculateAttribute((int)AttributeName);
    }

    public List<(string, float)> ReturnAttributeEffects(AttributesEnum AttributeName, bool isAdd)
    {
        if (isAdd)
        {
            return AttributesAdditiveEffects[(int)AttributeName];
        }
        else
        {
            return AttributesMultiplicativeEffects[(int)AttributeName];
        }
    }

    private void RecalculateAttribute(int AttributeLoc)
    {
        float temp = 0;

        foreach ((string, float) addTemp in AttributesAdditiveEffects[AttributeLoc])
        {
            temp += addTemp.Item2;
        }

        foreach ((string, float) multTemp in AttributesMultiplicativeEffects[AttributeLoc])
        {
            temp *= multTemp.Item2;
        }
        FinalAttributesStorage[AttributeLoc] = temp;
        UpdateExternalAttributes();
    }

    private void UpdateExternalAttributes()
    {
        PlayerHealth.modify_maxHealth(ReturnAttribute(AttributesEnum.max_health));
        PlayerHealth.modify_healthRegen(ReturnAttribute(AttributesEnum.health_regen));
        PlayerHealth.modify_Armor((int)ReturnAttribute(AttributesEnum.armor));
        PlayerHealth.modify_Plating((int)ReturnAttribute(AttributesEnum.plating));
        PlayerEnergy.modifymaxNormalEnergy(ReturnAttribute(AttributesEnum.max_energy));
        PlayerEnergy.modify_maxExpandedEnergy(ReturnAttribute(AttributesEnum.max_expanded_energy));
        PlayerEnergy.modify_energyNormal_regen(ReturnAttribute(AttributesEnum.energy_regen));
        PlayerEnergy.modify_energyExpanded_regen(ReturnAttribute(AttributesEnum.expanded_energy_regen));
    }
    //////////////////////////////////////



    //////////////////////////////////////
    private int[] NonCombatSkillsStorage = new int[7];
    private int[] CombatSkillsStorage = new int[3];

    public void ModifyNonCombatSkill(SkillsEnum SkillName, int value)
    {
        NonCombatSkillsStorage[(int)SkillName] = value;
    }

    public int ReturnNonCombatSkill(SkillsEnum SkillName)
    {
        return NonCombatSkillsStorage[(int)SkillName];
    }

    public void ModifyCombatSkill(CombatSkillsEnum SkillName, int value)
    {
        CombatSkillsStorage[(int)SkillName] = value;
        UpdateBaseAttributes();
    }

    public int ReturnCombatSkill(CombatSkillsEnum SkillName)
    {
        return CombatSkillsStorage[(int)SkillName];
    }
    //////////////////////////////////////


    private void Start()
    {
        Player = GameObject.Find("Player");
        PlayerHealth = Player.transform.GetComponent<Health>();
        PlayerEnergy = Player.transform.GetComponent<Energy>();
        combatChecker = Player.transform.GetComponentInChildren<CombatChecker>();
        uIController = GameObject.Find("UI").GetComponent<UIController>();
        eventQueue = GameObject.Find("EventDisplay").GetComponent<EventQueue>();

        for (int i = 0; i < STARTUP_DECLARATIONS.Number_of_Attributes; i++)
        {
            AttributesAdditiveEffects[i] = new List<(string, float)>();
            AttributesMultiplicativeEffects[i] = new List<(string, float)>();
        }
        level = 1;
        current_exp = 0;


        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Strength, 5);
        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Dexterity, 5);
        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Charisma, 5);
        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Intelligence, 5);
        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Toughness, 5);
        ModifyCoreIntrinsicSkill(CoreIntrinsicSkillsEnum.Willpower, 5);
        /////////////////////////////////////
        ModifyNonCombatSkill(SkillsEnum.Larceny, 50);
        /////////////////////////////////////
        AddAttributeEffect(AttributesEnum.max_health, "TEST", true, 50);
        AddAttributeEffect(AttributesEnum.armor, "TEST Armor", true, 10);
        AddAttributeEffect(AttributesEnum.plating, "TEST Plating", true, 5);
        ////////////////////////////////////
        AddEXP(975);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LevelUp();
    }
}
