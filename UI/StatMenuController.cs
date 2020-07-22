using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMenuController : MonoBehaviour
{
    [SerializeField] GameObject StatPrefab;
    [SerializeField] GameObject StatSpacerPrefab;
    [SerializeField] GameObject StatDeriv;
    private Transform StatsPanel;
    private Transform StatsInfoPanel;
    private Transform SystemStats;
    Transform InfoParent;
    private PlayerStats playerStats;

    private (string, string)[] attributeDesc = new (string, string)[STARTUP_DECLARATIONS.Number_of_Attributes];

    private bool first_enable;
    void Awake()
    {
        first_enable = true;
    }

    void OnEnable()
    {
        if (first_enable)
        {
            StatsPanel = transform.Find("StatsPanel");
            StatsInfoPanel = transform.Find("StatsInfoPanel");
            InfoParent = StatsInfoPanel.Find("Derivation");
            SystemStats = transform.Find("SystemStats");
            playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
            setupDescArrays();
            first_enable = false;
        }

        foreach (Transform child in StatsPanel)
        {
            Destroy(child.gameObject);
        }
        UpdateStatsInfo("", new List<(string, float)>(), new List<(string, float)>(), ("", ""));
        GameObject temp;

        temp = Instantiate(StatSpacerPrefab, StatsPanel);
        temp.GetComponentInChildren<Text>().text = "Attributes";
        for (int i = 0; i < STARTUP_DECLARATIONS.Number_of_Attributes; i++)
        {
            temp = Instantiate(StatPrefab, StatsPanel);
            string finalValue = playerStats.ReturnAttribute((AttributesEnum)i).ToString();
            List<(string, float)> AddDeriv = playerStats.ReturnAttributeEffects((AttributesEnum)i, true);
            List<(string, float)> MultDeriv = playerStats.ReturnAttributeEffects((AttributesEnum)i, false);
            temp.GetComponent<StatUIPrefab>().Setup(finalValue, AddDeriv, MultDeriv, attributeDesc[i]);
        }



        SystemStats.Find("LevelPanel").Find("LevelVar").GetComponent<Text>().text = (playerStats.returnLevel()).ToString();
    }

    private void setupDescArrays()
    {
        attributeDesc[(int)AttributesEnum.max_energy] = ("Energy", "The maximum amount of energy that you can have at anypoint. Energy is used for advanced movement, abilites, and attacks.");
        attributeDesc[(int)AttributesEnum.energy_regen] = ("Energy Regen", "The rate at which your energy is restored natrually. This is only active if your expanded energy is depleted.");
        attributeDesc[(int)AttributesEnum.max_expanded_energy] = ("Expanded Energy", "The maximum amount of expanded energy that you can have at anypoint. Expaned energy is used before your energy.");
        attributeDesc[(int)AttributesEnum.expanded_energy_regen] = ("Expanded Energy Regen", "The rate at which your expanded energy is restored natrually. This is only active if your expanded energy is not depleted.");
        attributeDesc[(int)AttributesEnum.max_health] = ("Health", "The maximum amount of damage you take before dying. Plating and Armor can reduce the amount of damage you take.");
        attributeDesc[(int)AttributesEnum.health_regen] = ("Health Regen", "The rate at which you natrually restore health.");
        attributeDesc[(int)AttributesEnum.armor] = ("Armor", "Reduces the amount of damage you take by a percentage. The damage reduction is calculated by (Armor) / (Armor + 100). True damage will ignore Armor.");
        attributeDesc[(int)AttributesEnum.plating] = ("Plating", "Reduces the amount of damage you take by a flat amount (calculated after Armor). True damage and elemental damage will ignore plating.");
    }

    public void UpdateStatsInfo(string finalValue, List<(string, float)> Add_in, List<(string, float)> Mult_in, (string, string) desc)
    {
        StatsInfoPanel.Find("Title").GetComponent<Text>().text = desc.Item1;
        StatsInfoPanel.Find("Description").GetComponent<Text>().text = desc.Item2;

        foreach(Transform child in InfoParent)
        {
            Destroy(child.gameObject);
        }

        foreach((string, float) i in Add_in)
        {
            string string_add = "";
            if(i.Item2 > 0)
            {
                string_add = "+";
            }
            InstantiateInfoPrefab(i.Item1, string_add + i.Item2.ToString());
        }

        foreach ((string, float) i in Mult_in)
        {
            string string_add = "";
            if (i.Item2 > 0)
            {
                string_add = "+";
            }
            InstantiateInfoPrefab(i.Item1, (string_add + (i.Item2 - 1) * 100).ToString() + "%");
        }

        InstantiateInfoPrefab("Final Value: ", finalValue, true);
    }

    private void InstantiateInfoPrefab(string Name, string Var, bool special = false)
    {
        if(Var != "")
        {
            GameObject temp = Instantiate(StatDeriv, InfoParent);
            temp.transform.Find("Name").GetComponent<Text>().text = Name;
            temp.transform.Find("Var").GetComponent<Text>().text = Var;

            if (special)
            {
                temp.GetComponent<Image>().color = Color.red;
            }
        }
    }

}
