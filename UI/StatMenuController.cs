using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMenuController : MonoBehaviour
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


    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        Strength = transform.Find("Strength").Find("Text").gameObject;
        Dexterity = transform.Find("Dexterity").Find("Text").gameObject;
        Charisma = transform.Find("Charisma").Find("Text").gameObject;
        Intelligence = transform.Find("Intelligence").Find("Text").gameObject;
        Toughness = transform.Find("Toughness").Find("Text").gameObject;
        Willpower = transform.Find("Willpower").Find("Text").gameObject;
        Vigor = transform.Find("Vigor").Find("Text").gameObject;
        Cerebral = transform.Find("Cerebral").Find("Text").gameObject;
        Fortitude = transform.Find("Fortitude").Find("Text").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Strength.GetComponent<Text>().text =        (playerStats.ReturnCoreStat(CoreStatsEnum.Strength)).ToString();
        Dexterity.GetComponent<Text>().text =       (playerStats.ReturnCoreStat(CoreStatsEnum.Dexterity)).ToString();
        Charisma.GetComponent<Text>().text =        (playerStats.ReturnCoreStat(CoreStatsEnum.Charisma)).ToString();
        Intelligence.GetComponent<Text>().text =    (playerStats.ReturnCoreStat(CoreStatsEnum.Intelligence)).ToString();
        Toughness.GetComponent<Text>().text =       (playerStats.ReturnCoreStat(CoreStatsEnum.Toughness)).ToString();
        Willpower.GetComponent<Text>().text =       (playerStats.ReturnCoreStat(CoreStatsEnum.Willpower)).ToString();

        Vigor.GetComponent<Text>().text =           (playerStats.ReturnDerivedStat(DerivedStatsEnum.Vigor)).ToString();
        Cerebral.GetComponent<Text>().text =        (playerStats.ReturnDerivedStat(DerivedStatsEnum.Cerebral)).ToString();
        Fortitude.GetComponent<Text>().text =       (playerStats.ReturnDerivedStat(DerivedStatsEnum.Fortitude)).ToString();
    }
}
