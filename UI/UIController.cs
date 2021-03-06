﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject HUD;
    public GameObject Map;
    public GameObject InteractiveObjectMenu;
    public GameObject LevelUpMenu;
    public GameObject DialogueMenu;

    public bool GamePaused;

    private bool inescapableExternalMenu;
    private bool ExternalMenuOpen;
    private float originalTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        inescapableExternalMenu = false;
        ExternalMenuOpen = false;
        originalTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        GamePaused = !HUD.activeSelf;

        // TODO: Handle Input through InputManager and not direct key references
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inescapableExternalMenu)
            {
                //Do Nothing
            }
            else if (ExternalMenuOpen)
            {
                ExternalMenuOpen = false;
                InteractiveObjectMenu.SetActive(false);
                Unpaused();
            }
            else
            {
                Menu.SetActive(!Menu.activeSelf);
                if (Menu.activeSelf == true)
                {
                    Paused();
                }
                else
                {
                    Unpaused();
                }
            }
        }

        // TODO: Handle Input through InputManager and not direct key references
        if (!inescapableExternalMenu && !ExternalMenuOpen && Input.GetKeyDown(KeyCode.Tab))
        {
            if (Menu.activeSelf)
            {
                Menu.GetComponent<MenuController>().WorldEnable();                
            }
            else
            {
                Map.SetActive(!Map.activeSelf);
            }
        }
    }

    public void OpenInteractiveMenu(GameObject container)
    {
        Paused();
        Map.SetActive(false);
        ExternalMenuOpen = true;
        InteractiveObjectMenu.SetActive(true);
        InteractiveObjectMenu.GetComponent<InteractiveObjectMenuUI>().Refresh(container);
    }

    public void LevelUpMenuBool(bool open)
    {
        if(open)
        {
            Paused();
            Map.SetActive(false);
            inescapableExternalMenu = true;
            LevelUpMenu.SetActive(true);
        }
        else
        {
            Unpaused();
            inescapableExternalMenu = false;
            LevelUpMenu.SetActive(false);
        }
    }

    public void DialogueMenuBool(Transform DiaData = null)
    {
        if (DiaData != null)
        {
            Paused();
            Map.SetActive(false);
            inescapableExternalMenu = true;
            DialogueMenu.SetActive(true);
            DialogueMenu.GetComponent<DiaParent>().SetupDia(DiaData);
        }
        else
        {
            Unpaused();
            inescapableExternalMenu = false;
            DialogueMenu.SetActive(false);
        }
    }

    void Paused()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
    }

    void Unpaused()
    {
        Time.timeScale = originalTimeScale;
        HUD.SetActive(true);
    }
}
