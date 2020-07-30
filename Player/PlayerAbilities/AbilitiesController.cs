using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] private List<Ability> abilities = new List<Ability>();
     private GameObject AbilitiesBar;


    private void Start()
    {
        AbilitiesBar = GameObject.Find("Abilities Bar");

        int i = 0;
        foreach (Transform child in AbilitiesBar.transform.Find("TextPanel"))
        {
            child.Find("Image").GetComponent<Image>().sprite = abilities[i].ability_sprite;
            ++i;
        }
    }

    private void Update()
    {
        int i = 0;
        foreach(Transform child in AbilitiesBar.transform.Find("TextPanel"))
        {
            float cd_remaining = abilities[i].cooldown_remaining;
            if(cd_remaining > 0)
            {
                child.Find("Text").GetComponent<TextMeshProUGUI>().text = cd_remaining.ToString("0");
                child.Find("Darken").gameObject.SetActive(true);
            }
            else
            {
                child.Find("Text").GetComponent<TextMeshProUGUI>().text = "";
                child.Find("Darken").gameObject.SetActive(false);
            }
            ++i;
        }
    }

    // TODO: Handle Input through InputManager and not direct key references
    public void HandleAbilities(float time_dia)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && abilities.Count > 0)
        {
            abilities[0].AttemptAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && abilities.Count > 1)
        {
            abilities[1].AttemptAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && abilities.Count > 2)
        {
            abilities[2].AttemptAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && abilities.Count > 3)
        {
            abilities[3].AttemptAttack();
        }
    }
}
