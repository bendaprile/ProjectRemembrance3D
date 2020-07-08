using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    private GameObject Player;

    //////////////////////////////////////

    private int[] CoreStatsStorage = new int[6];


    private int[] DerivedStatsStorage = new int[3];

    public void ModifyCoreStat(CoreStatsEnum CoreStatName, int value)
    {
        CoreStatsStorage[(int)CoreStatName] = value;
        UpdateDerivedStats();
    }
    public int ReturnCoreStat(CoreStatsEnum CoreStatName)
    {
        return CoreStatsStorage[(int)CoreStatName];
    }
    public int ReturnDerivedStat(DerivedStatsEnum DerivedStatName)
    {
        return DerivedStatsStorage[(int)DerivedStatName];
    }
    //////////////////////////////////////


    //////////////////////////////////////
    private float[] AttributesStorage = new float[6];
    public float ReturnAttribute(AttributesEnum AttributeName)
    {
        return AttributesStorage[(int)AttributeName];
    }
    //////////////////////////////////////


    //////////////////////////////////////
    private int[] SkillsStorage = new int[1];

    public void ModifySkill(SkillsEnum SkillName, int value)
    {
        SkillsStorage[(int)SkillName] = value;
    }
    public int ReturnSkill(SkillsEnum SkillName)
    {
        return SkillsStorage[(int)SkillName];
    }
    //////////////////////////////////////



    private void UpdateDerivedStats()
    {
        DerivedStatsStorage[(int)DerivedStatsEnum.Vigor] = CoreStatsStorage[(int)CoreStatsEnum.Strength] + CoreStatsStorage[(int)CoreStatsEnum.Dexterity];
        DerivedStatsStorage[(int)DerivedStatsEnum.Cerebral] = CoreStatsStorage[(int)CoreStatsEnum.Charisma] + CoreStatsStorage[(int)CoreStatsEnum.Intelligence];
        DerivedStatsStorage[(int)DerivedStatsEnum.Fortitude] = CoreStatsStorage[(int)CoreStatsEnum.Toughness] + CoreStatsStorage[(int)CoreStatsEnum.Willpower];
    }

    public void UpdateAttributes()
    {

    }


    private void Start()
    {
        Player = GameObject.Find("Player");

        ModifyCoreStat(CoreStatsEnum.Strength, 5);
        ModifyCoreStat(CoreStatsEnum.Dexterity, 5);
        ModifyCoreStat(CoreStatsEnum.Charisma, 5);
        ModifyCoreStat(CoreStatsEnum.Intelligence, 5);
        ModifyCoreStat(CoreStatsEnum.Toughness, 5);
        ModifyCoreStat(CoreStatsEnum.Willpower, 5);
        ModifySkill(SkillsEnum.Lockpicking, 50);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
