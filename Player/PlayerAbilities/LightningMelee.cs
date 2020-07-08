using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMelee : Ability
{
    public GameObject Lightning;
    [SerializeField] float damage = 2.5f;
    [SerializeField] float damage_duration = .2f;
    [SerializeField] float timeout = 10f;
    [SerializeField] private float energyCost = 100;

    [SerializeField] int charge_increment = 7;
    [SerializeField] int max_targets = 3;

    [SerializeField] Transform LightningStartPos = null;


    public int CurrentCharges;

    private int assignment_counter;

    private Energy energy;
    private Transform PlayerProjectiles;
    private float time_keeper;


    private float[] TimerArray;
    private Collider[] ColArray;
    private GameObject[] lightningArray;
    private Transform[] lightningArrayStarting;
    private Transform[] lightningArrayEnding;
    private Collider[] enemies;

    void Start()
    {
        time_keeper = 0f;
        CurrentCharges = 0;
        assignment_counter = 0;
        energy = GameObject.Find("Player").GetComponent<Energy>();

        PlayerProjectiles = GameObject.Find("PlayerProjectiles").transform;


        TimerArray = new float[max_targets];
        ColArray = new Collider[max_targets];

        lightningArray = new GameObject[max_targets];
        lightningArrayStarting = new Transform[max_targets];
        lightningArrayEnding = new Transform[max_targets];
        for (int i = 0; i < max_targets; i++)
        {
            TimerArray[i] = 0;
            lightningArray[i] = Instantiate(Lightning, transform.position, Quaternion.identity);
            lightningArray[i].transform.parent = gameObject.transform;

            lightningArrayStarting[i] = lightningArray[i].transform.Find("LightningStart");
            lightningArrayEnding[i] = lightningArray[i].transform.Find("LightningEnd");
        }
    }

    public override void Attack(float time_dia, float master_counter)
    {
        if (CurrentCharges == 0)
        {
            if (energy.drain_energy(energyCost))
            {
                time_keeper = 0f;
                CurrentCharges = charge_increment;
            }
        }
    }


    public void MeleeAttack(Collider col)
    {
        ColArray[assignment_counter] = col;
        if (CurrentCharges > 0)
        {
            TimerArray[assignment_counter] = damage_duration;
        }
        else
        {
            TimerArray[assignment_counter] = 0;
        }
        assignment_counter = (assignment_counter + 1) % max_targets;
    }


    void FixedUpdate()
    {
        if(time_keeper >= timeout)
        {
            CurrentCharges = 0;
        }
        time_keeper += Time.fixedDeltaTime;


        for (int i = 0; i < max_targets; i++)
        {
            if (CurrentCharges > 0)
            {
                lightningArrayStarting[i].position = LightningStartPos.position;
                lightningArray[i].SetActive(true);

                if (time_keeper > (Time.fixedDeltaTime / 2)) //Start a frame later to fix visual glitch
                {
                    lightningArray[i].GetComponent<LineRenderer>().enabled = true;
                }


                if (TimerArray[i] > 0)
                {
                    TimerArray[i] -= Time.fixedDeltaTime;

                    if(TimerArray[i] <= 0)
                    {
                        --CurrentCharges;
                    }

                    lightningArrayEnding[i].position = ColArray[i].transform.position;
                    ColArray[i].gameObject.GetComponent<Health>().take_damage(damage, DT: DamageType.Elemental, isDoT: true);

                }
                else
                {
                    Vector3 endpos = LightningStartPos.position;
                    endpos += new Vector3(.5f * Random.value - .25f, .5f * Random.value - .25f, .5f * Random.value - .25f);
                    lightningArrayEnding[i].position = endpos;
                }
            }
            else
            {
                lightningArray[i].GetComponent<LineRenderer>().enabled = false;
                lightningArray[i].SetActive(false);
            }
        }
    }
}
