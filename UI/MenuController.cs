using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject Map;
    [SerializeField] Color HubButtonResetColor;
    private GameObject HubMenu;

    private GameObject WorldMenu;
    private GameObject InventoryMenu;
    private GameObject StatsMenu;
    private GameObject SkillMenu;
    private GameObject AbilitiesMenu;
    private GameObject BackgroundImage;

    private CombatChecker CombatChecker;

    private bool pulsate;


    private void OnEnable()
    {
        CombatChecker = GameObject.Find("CombatChecker").GetComponent<CombatChecker>();
        BackgroundImage = transform.Find("Image").gameObject;
        HubMenu = transform.Find("HubMenu").gameObject;
        SkillMenu = transform.Find("Panel").Find("SkillMenu").gameObject;
        StatsMenu = transform.Find("Panel").Find("StatsMenu").gameObject;
        WorldMenu = transform.Find("Panel").Find("WorldMenu").gameObject;
        InventoryMenu = transform.Find("Panel").Find("InventoryMenu").gameObject;
        AbilitiesMenu = transform.Find("Panel").Find("AbilitiesMenu").gameObject;

        if (CombatChecker.enemies_nearby)
        {
            pulsate = true;
        }
        else
        {
            pulsate = false;
            BackgroundImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, .4f);
        }

        if (Map.activeSelf) //This brings the map in front of the image
        {
            MassDisable();
            WorldEnable();
        }
        else
        {
            MassDisable();
        }
    }

    private void Update()
    {
        if (pulsate)
        {
            float redness = 0.2f * (2f + Mathf.Sin((float)Time.unscaledTime*.5f)); //goes between .1 and .2
            BackgroundImage.GetComponent<Image>().color = new Color(redness, 0f, 0f, .9375f);
        }

    }

    private void OnDisable()
    {
        Map.SetActive(false);
    }

    public void SkillEnable()
    {
        MassDisable();
        SkillMenu.SetActive(true);
        HubMenu.transform.Find("SkillButton").GetComponent<Image>().color = Color.red;
    }

    public void StatsEnable()
    {
        MassDisable();
        StatsMenu.SetActive(true);
        HubMenu.transform.Find("StatsButton").GetComponent<Image>().color = Color.red;
    }

    public void WorldEnable()
    {
        MassDisable();
        WorldMenu.SetActive(true);
        HubMenu.transform.Find("WorldButton").GetComponent<Image>().color = Color.red;
        Map.SetActive(true);
    }

    public void AbilitiesEnable()
    {
        MassDisable();
        AbilitiesMenu.SetActive(true);
        HubMenu.transform.Find("AbilitiesButton").GetComponent<Image>().color = Color.red;
    }

    public void InventoryEnable()
    {
        MassDisable();
        InventoryMenu.SetActive(true);
        HubMenu.transform.Find("InventoryButton").GetComponent<Image>().color = Color.red;
    }

    private void MassDisable()
    {
        SkillMenu.SetActive(false);
        StatsMenu.SetActive(false);
        WorldMenu.SetActive(false);
        Map.SetActive(false);
        InventoryMenu.SetActive(false);
        ResetColors();

    }

    private void ResetColors()
    {
        foreach (Transform child in HubMenu.transform)
        {
            child.GetComponent<Image>().color = HubButtonResetColor;
        }
    }
}
