using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    [SerializeField] float range = 0;
    [SerializeField] float delay = 0;
    [SerializeField] float hitbox_width = 0;
    [SerializeField] float line_y_bias = 0;

    [SerializeField] float velocity = 0;

    [SerializeField] float duration = 0;
    [SerializeField] float damage = 0;

    [SerializeField] Rigidbody parent_rb = null;
    [SerializeField] Transform parent_transform = null;

    public bool isCharging;

    private GameObject player;
    private Vector3 dir;
    private LineRenderer lr;
    private Transform lineTrans;
    private Collider sc;

    void Start()
    {
        isCharging = false;
        player = GameObject.Find("Player");
        lr = GetComponentInChildren<LineRenderer>();
        lineTrans = GetComponentInChildren<Transform>();
        sc = GetComponent<Collider>();
        sc.enabled = false;
        lr.enabled = false;
        make_rect();
    }

    public bool Charge()
    {
        if (clean_Forward_LoS())
        {
            isCharging = true;
            mod_rect(0f);
            lr.enabled = true;
            StartCoroutine(ChargeMechanics());
            return true;
        }
        return false;
    }

    private IEnumerator ChargeMechanics()
    {
        float z_axis = 0;
        for (float i = 0; i < delay; i += Time.fixedDeltaTime)
        {
            z_axis += duration * velocity * Time.fixedDeltaTime / (delay);
            mod_rect(z_axis);
            dir = player.transform.position - transform.position;
            dir.y = 0;
            dir = dir.normalized;
            parent_transform.rotation = Quaternion.RotateTowards(parent_transform.rotation, Quaternion.Euler(new Vector3(0f, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0f)), 720 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        RigidbodyConstraints original_constraints = parent_rb.constraints;
        parent_rb.constraints = original_constraints | RigidbodyConstraints.FreezeRotationY;

        sc.enabled = true;
        for (float i = 0; i < duration; i += Time.fixedDeltaTime)
        {
            z_axis -= velocity * Time.fixedDeltaTime;
            mod_rect(z_axis);
            parent_rb.velocity = dir * velocity;
            if (!clean_LoS())
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        parent_rb.constraints = original_constraints;
        sc.enabled = false;
        lr.enabled = false;
        isCharging = false;
    }

    private bool clean_Forward_LoS() //Has direct Forward Line of Sight to the player
    {
        int layerMask = (LayerMask.GetMask("Projectile") | LayerMask.GetMask("Ignore Raycast"));
        layerMask = ~layerMask; //ignore Projectile

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private bool clean_LoS() //Has direct Line of Sight to the player
    {
        int layerMask = (LayerMask.GetMask("Projectile") | LayerMask.GetMask("Ignore Raycast"));
        layerMask = ~layerMask; //ignore Projectile

        Vector3 New_dir = player.transform.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, New_dir, out hit, range*2, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private void make_rect()
    {
        int segments = 4;
        float max_dist = velocity * duration;
        lr.positionCount = segments + 1;
        lr.useWorldSpace = false;

        lr.SetPosition(0, new Vector3(-hitbox_width / 2, line_y_bias, 0));
        lr.SetPosition(1, new Vector3(-hitbox_width / 2, line_y_bias, 0));
        lr.SetPosition(2, new Vector3(hitbox_width / 2, line_y_bias, 0));
        lr.SetPosition(3, new Vector3(hitbox_width / 2, line_y_bias, 0));
        lr.SetPosition(4, new Vector3(-hitbox_width / 2, line_y_bias, 0));
    }

    private void mod_rect(float z_axis)
    {
        lr.SetPosition(1, new Vector3(-hitbox_width / 2, line_y_bias, z_axis));
        lr.SetPosition(2, new Vector3(hitbox_width / 2, line_y_bias, z_axis));
    }


    void OnTriggerEnter(Collider other) //Uses physics time
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().take_damage(damage);
        }
    }
}
