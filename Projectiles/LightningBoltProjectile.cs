using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LightningBoltProjectile: MonoBehaviour
{
    public float AttackDelay = 1f;
    public float damage = 0f; //Ticks 13 times at .25
    public float time_dia = 1f;
    public float radius = 4f;
    public bool enemy_strike;
    public Material[] myMaterials;

    private GameObject preattack_loc;
    private GameObject attack_loc;


    private float real_volume;
    private bool audio_ending;
    private AudioSource audio_var;

    private bool deal_damage;

    private LineRenderer line;


    void Start()
    {
        audio_ending = false;
        audio_var = this.GetComponent<AudioSource>();
        real_volume = audio_var.volume;

        make_circle();

        attack_loc = transform.Find("Lightning Bolt Attack").gameObject;
        preattack_loc = transform.Find("Lightning Bolt preAttack").gameObject;

        GetComponent<SphereCollider>().radius = radius;

        attack_loc.SetActive(false);
        deal_damage = false;

        StartCoroutine(attack());
    }

    private void make_circle()
    {
        int segments = 20;
        line = GetComponentInChildren<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        float angle = 0f;
        float x;
        float z;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            line.SetPosition(i,new Vector3(x, 0, z));

            angle += (360f / segments);
        }

        if (enemy_strike)
        {
            line.material = myMaterials[1];
        }
        else
        {
            line.material = myMaterials[0];
        }

    }


    private IEnumerator attack()
    {
        audio_var.enabled = true;
        audio_var.time = 6 - (AttackDelay);
        audio_var.pitch = time_dia;

        yield return new WaitForSeconds(AttackDelay / time_dia);

        attack_loc.SetActive(true);
        deal_damage = true;
        preattack_loc.SetActive(false);

        yield return new WaitForSeconds(.25f / time_dia); //Duration

        deal_damage = false;
        attack_loc.SetActive(false);
        audio_ending = true;

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private void Update()
    {
        float expand = 1 / (AttackDelay / time_dia);

        preattack_loc.transform.localScale += new Vector3(expand, expand, expand) * Time.deltaTime;

        if (audio_ending)
        {
            audio_var.volume -= real_volume * Time.deltaTime;
        }
    }

    void OnTriggerStay(Collider other) //Uses physics time
    {
        if (deal_damage)
        {
            if (enemy_strike && other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Health>().take_damage(damage);
            }
            if (!enemy_strike && other.gameObject.tag == "BasicEnemy")
            {
                other.gameObject.GetComponent<Health>().take_damage(damage);
            }
        }
    }
}
