using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : Ability
{
    [SerializeField] private float energyCost = 40f;
    [SerializeField] private float duration = 4f;
    [SerializeField] private float cdDelay = 10f;
    [SerializeField] private float ratio = .5f;

    private Energy energy;
    private float originalTimeScale;
    private float usable_time;

    private bool dilationActive;
    private float ending_countdown;

    void Start()
    {
        energy = GameObject.Find("Player").GetComponent<Energy>();

        usable_time = 0f;
        dilationActive = false;
        originalTimeScale = Time.timeScale;
    }



    public override void Attack(float time_dia, float master_counter)
    {
        if (master_counter >= usable_time)
        {
            if(energy.drain_energy(energyCost))
            {
                dilationActive = true;
                ending_countdown = duration;
                usable_time = master_counter + cdDelay;
                Time.timeScale = ratio * originalTimeScale;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(dilationActive)
        {
            ending_countdown -= Time.deltaTime;
            if(ending_countdown <= 0)
            {
                dilationActive = false;
                Time.timeScale = originalTimeScale;
            }
        }
    }
}
