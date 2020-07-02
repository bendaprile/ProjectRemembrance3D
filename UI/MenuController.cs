using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject Map;
    [SerializeField] Color HubButtonResetColor;
    private GameObject HubMenu;

    private GameObject InventoryMenu;
    private GameObject StatsMenu;
    private GameObject SkillsMenu;
    private GameObject AbilitiesMenu;
    private GameObject BackgroundImage;

    private CombatChecker CombatChecker;


    private void OnEnable()
    {
        CombatChecker = GameObject.Find("CombatChecker").GetComponent<CombatChecker>();
        BackgroundImage = transform.Find("Image").gameObject;
        HubMenu = transform.Find("HubMenu").gameObject;
        StatsMenu = transform.Find("StatsMenu").gameObject;
        InventoryMenu = transform.Find("InventoryMenu").gameObject;
        SkillsMenu = transform.Find("SkillsMenu").gameObject;
        AbilitiesMenu = transform.Find("AbilitiesMenu").gameObject;


        if (CombatChecker.enemies_nearby)
        {
            BackgroundImage.GetComponent<Image>().color = new Color(.5f, 0f, 0f, .4f);
        }
        else
        {
            BackgroundImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, .4f);
        }

        if (Map.activeSelf) //This brings the map in front of the image
        {
            MassDisable();
            MapEnable();
        }
        else
        {
            MassDisable();
        }
    }

    private void OnDisable()
    {
        Map.SetActive(false);
    }

    public void StatsEnable()
    {
        MassDisable();
        StatsMenu.SetActive(true);
        HubMenu.transform.Find("StatsButton").GetComponent<Image>().color = Color.red;
    }

    public void MapEnable()
    {
        MassDisable();
        Map.SetActive(true);
        HubMenu.transform.Find("MapButton").GetComponent<Image>().color = Color.red;
    }

    public void AbilitiesEnable()
    {
        MassDisable();
        AbilitiesMenu.SetActive(true);
        HubMenu.transform.Find("AbilitiesButton").GetComponent<Image>().color = Color.red;
    }


    public void SkillsEnable()
    {
        MassDisable();
        SkillsMenu.SetActive(true);
        HubMenu.transform.Find("SkillsButton").GetComponent<Image>().color = Color.red;
    }

    public void InventoryEnable()
    {
        MassDisable();
        InventoryMenu.SetActive(true);
        HubMenu.transform.Find("InventoryButton").GetComponent<Image>().color = Color.red;
    }

    private void MassDisable()
    {
        StatsMenu.SetActive(false);
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
