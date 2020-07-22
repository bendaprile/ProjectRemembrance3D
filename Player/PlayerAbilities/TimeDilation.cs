using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : Ability
{
    [SerializeField] private float duration = 4f;
    [SerializeField] private float ratio = .5f;


    private float originalTimeScale;
    private float usable_time;

    private bool dilationActive;
    private float ending_countdown;

    void Start()
    {
        dilationActive = false;
        originalTimeScale = Time.timeScale;
    }



    protected override void Attack()
    {
        dilationActive = true;
        ending_countdown = duration;
        Time.timeScale = ratio * originalTimeScale;
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
