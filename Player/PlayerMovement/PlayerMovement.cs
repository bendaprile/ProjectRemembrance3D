using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float baseSpeed = 6f;
    [SerializeField] float runSpeedMultiplier = 1.7f;
    [SerializeField] float rollSpeedMultiplier = 2.5f;
    [SerializeField] float meleeMoveSpeedMultiplier = 0.5f;

    [SerializeField] float rollEnergyCost = 40f;
    [SerializeField] float MinSprintEnergy = 5f;
    [SerializeField] float sprintEnergyCost = 0.2f;

    private Vector3 heading, forward, side, force;
    private MoveState moveState = MoveState.Idle;

    private Rigidbody rb;
    private PlayerAnimationUpdater animationUpdater;
    private Energy energy;

    private float heldRotation;

    public Vector3 mousePos3d;
    public bool itemEquipped;
    public bool postAnimation = false;



    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        heldRotation = 0f;

        forward = Vector3.Normalize(forward);
        side = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        rb = GetComponent<Rigidbody>();
        animationUpdater = GetComponentInChildren<PlayerAnimationUpdater>();
        energy = GameObject.Find("Player").GetComponent<Energy>();
    }
    
    public void Move()
    {
        // Prevents the player from changing direction while rolling
        if (moveState != MoveState.Rolling)
        {
            heading = side * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical");
            heading = Vector3.Normalize(new Vector3(heading.x, 0, heading.z));
        }
        
        Vector3 desired_movement = (heading * GetMoveSpeed() - rb.velocity);
        if(desired_movement.magnitude > 1f)
        {
            desired_movement = desired_movement.normalized;
            force = 50f * desired_movement;
        }
        else
        {
            force = 50f * desired_movement;
        }


        if (itemEquipped)
        {
            // TODO: Handle for controllers
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitray;

            int layerMask = (LayerMask.GetMask("Terrain"));
            if (Physics.Raycast(ray, out hitray, Mathf.Infinity, layerMask))
            {
                mousePos3d = hitray.point;
            }
            float look_angle = Mathf.Atan2(mousePos3d.x - transform.position.x, mousePos3d.z - transform.position.z) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, look_angle, 0));
        }
        else
        {
            if (heading.magnitude != 0)
            {
                heldRotation = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, heldRotation, 0)), 720 * Time.fixedDeltaTime);
        }

        rb.AddForce(new Vector3(force.x, 0, force.z));
    }

    private float GetMoveSpeed()
    {
        switch (moveState)
        {
            case MoveState.Idle:
                return 0f;
            case MoveState.Walking:
                return baseSpeed;
            case MoveState.Running:
                return baseSpeed * runSpeedMultiplier;
            case MoveState.Rolling:
                return baseSpeed * rollSpeedMultiplier;
            case MoveState.Melee:
                return baseSpeed * meleeMoveSpeedMultiplier;
            default:
                Debug.LogError("Player was not in a movement state... ");
                return 0f;
        }
    }

    public void UpdatePlayerState()
    {

        if (!postAnimation)
        {
            if (moveState == MoveState.Rolling)
            {
                return;
            }
            else if (moveState == MoveState.Melee){
                return;
            }
        }
        else
        {
            moveState = MoveState.Idle;
        }

        // TODO: Handle for controllers
        float controlThrowX = Mathf.Abs(Input.GetAxis("Horizontal"));
        float controlThrowY = Mathf.Abs(Input.GetAxis("Vertical"));

        if (controlThrowX > 0 || controlThrowY > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && moveState != MoveState.Idle)
            {
                if (energy.drain_energy(rollEnergyCost))
                {
                    moveState = MoveState.Rolling;
                    animationUpdater.RollAnimation();
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift) && energy.drain_energy_greaterThan(sprintEnergyCost, (moveState != MoveState.Running), MinSprintEnergy))
            {
                moveState = MoveState.Running;
            }
            else
            {
                moveState = MoveState.Walking;
            }
        }
        else if (controlThrowX == 0 && controlThrowY == 0)
        {
            moveState = MoveState.Idle;
        }

        postAnimation = false;
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
    }

    public void AddForwardForce(float force)
    {
        rb.AddForce(transform.forward * force);
    }

    public void SetMeleeState()
    {
        moveState = MoveState.Melee;
    }

    public MoveState GetMoveState()
    {
        return moveState;
    }
}
