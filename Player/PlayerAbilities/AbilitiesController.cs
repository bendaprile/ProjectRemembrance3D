using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{

    [SerializeField] private List<Ability> abilities = new List<Ability>();

    float master_counter;

    private void Start()
    {
        master_counter = 0;
    }

    private void Update()
    {
        master_counter += Time.deltaTime;
    }



    public void HandleAbilities(float time_dia)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && abilities.Count > 0)
        {
            abilities[0].Attack(time_dia, master_counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && abilities.Count > 1)
        {
            abilities[1].Attack(time_dia, master_counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && abilities.Count > 2)
        {
            abilities[2].Attack(time_dia, master_counter);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && abilities.Count > 3)
        {
            abilities[3].Attack(time_dia, master_counter);
        }
    }
}
