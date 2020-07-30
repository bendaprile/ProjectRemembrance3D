using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

    [SerializeField] private Light light_comp0 = null;
    [SerializeField] private Light light_comp1 = null;

    [SerializeField] float distance = 2f;
    [SerializeField] float height = 4f;

    private Transform playerTrans;
    private  Rigidbody rb;

    private SphereCollider SC;

    private PlayerMovement pm;
    private void Start()
    {
        playerTrans = GameObject.Find("Player").transform;
        pm = playerTrans.gameObject.GetComponent<PlayerMovement>();

        SC = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rotation();
        ControlFlashlight();
    }
    private void FixedUpdate()
    {
        Vector3 vector = playerTrans.position - transform.position;
        vector.y = 0f;

        float vecMag = vector.magnitude;

        if(vecMag > 10)
        {
            SC.enabled = false;
        }
        else
        {
            SC.enabled = true;
        }


        if (vecMag > distance)
        {
            Vector3 desired_movement = new Vector3(vector.x * 20f, 0f, vector.z * 20f) - rb.velocity; //Non-constant
            Vector3 forces = desired_movement / 3;
            rb.AddForce(forces);
        }

        Vector3 dronePos = transform.position;
        dronePos.y = playerTrans.position.y + height;
        transform.position = dronePos;
    }

    // TODO: Handle Input through InputManager and not direct key references
    void ControlFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            light_comp0.enabled = !light_comp0.enabled;
            light_comp1.enabled = !light_comp1.enabled;
        }
    }


    private void rotation()
    {
        float angle;
        float rotation_speed;
        if (pm.itemEquipped && light_comp0.enabled)
        {
            angle = Mathf.Atan2(pm.mousePos3d.x - transform.position.x, pm.mousePos3d.z - transform.position.z) * Mathf.Rad2Deg;
            rotation_speed = 360 * Time.fixedDeltaTime;
        }
        else
        {
            angle = Mathf.Atan2(playerTrans.position.x - transform.position.x, playerTrans.position.z - transform.position.z) * Mathf.Rad2Deg;
            rotation_speed = 90 * Time.fixedDeltaTime;
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), rotation_speed);
    }
}
