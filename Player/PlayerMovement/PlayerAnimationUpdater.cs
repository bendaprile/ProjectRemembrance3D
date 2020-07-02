using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationUpdater : MonoBehaviour
{
    public bool postAnimation = false;

    [SerializeField] float transitionFade = 0.25f;
    string currentAnim = "idle";
    Direction.MoveDir moveDirection;
    bool dancing = false;

    // Stored references
    Animator animator;
    PlayerMovement playerMovement;
    Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        direction = GetComponent<Direction>();
    }

    public void UpdatePlayerDirection()
    {
        if (!playerMovement.itemEquipped)
        {
            gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            return;
        }

        float yRotation = direction.AngleToDirection();

        if(playerMovement.moveState != PlayerMovement.MoveState.Idle && playerMovement.moveState != PlayerMovement.MoveState.Melee)
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(new Vector3(0, yRotation, 0)), 90 * Time.fixedDeltaTime);
        }

    }

    public void UpdateAnimation()
    {
        moveDirection = direction.GetMoveDirection();

        if (CanUpdateAnimation())
        {
            UpdateMoveAnimation();
            DanceAnimation();
        }
    }

    public void UpdateMoveAnimation()
    {
        float controlThrowX = Mathf.Abs(Input.GetAxis("Horizontal"));
        float controlThrowY = Mathf.Abs(Input.GetAxis("Vertical"));

        if (controlThrowX > 0 || controlThrowY > 0)
        {
            dancing = false;
            if (playerMovement.moveState == PlayerMovement.MoveState.Running)
            {
                UpdateRunAnimation();
            }
            else
            {
                UpdateWalkAnimation();
            }
        }
        else if (dancing == false)
        {
            PlayAnimation("idle");
        }
    }

    void UpdateRunAnimation()
    {
        if (playerMovement.itemEquipped)
        {
            switch (moveDirection)
            {
                case Direction.MoveDir.Forward:
                    PlayAnimation("run_f_1h");
                    break;
                case Direction.MoveDir.Right:
                    PlayAnimation("run_r_1h");
                    break;
                case Direction.MoveDir.Left:
                    PlayAnimation("run_l_1h");
                    break;
                case Direction.MoveDir.Backward:
                    PlayAnimation("run_b_1h");
                    break;
            }
        }
        else
        {
            PlayAnimation("run_f_1h");
        }
    }

    void UpdateWalkAnimation()
    {
        if (playerMovement.itemEquipped)
        {
            switch (moveDirection)
            {
                case Direction.MoveDir.Forward:
                    PlayAnimation("walk_f_1h");
                    break;
                case Direction.MoveDir.Right:
                    PlayAnimation("walk_r_1h");
                    break;
                case Direction.MoveDir.Left:
                    PlayAnimation("walk_l_1h");
                    break;
                case Direction.MoveDir.Backward:
                    PlayAnimation("walk_b_1h");
                    break;
            }
        }
        else
        {
            PlayAnimation("walk_f_1h");
        }
    }

    public void RollAnimation()
    { 

        if (playerMovement.itemEquipped)
        {
            switch (moveDirection)
            {
                case Direction.MoveDir.Forward:
                    PlayAnimation("roll_f");
                    break;
                case Direction.MoveDir.Backward:
                    PlayAnimation("roll_b");
                    break;
                case Direction.MoveDir.Left:
                    PlayAnimation("roll_l");
                    break;
                case Direction.MoveDir.Right:
                    PlayAnimation("roll_r");
                    break;
            }
        }
        else
        {
            PlayAnimation("roll_f");
        }
    }

    void DanceAnimation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            dancing = true;
            PlayAnimation("dance");
        }
    }

    public void PlayAnimation(string animName, bool rootMotion = false)
    {
        if (currentAnim != animName)
        {
            animator.applyRootMotion = rootMotion;

            animator.CrossFadeInFixedTime(animName, transitionFade);
            currentAnim = animName;
        }
    }

    public void PlayUpperBodyAnimation(string animName)
    {
        animator.CrossFadeInFixedTime(animName, transitionFade, 1);
        currentAnim = animName;
    }

    public void RollAnimationOver()
    {
        playerMovement.postAnimation = true;
    }

    public void DisableRootMotion()
    {
        animator.applyRootMotion = false;
    }

    public void OnAnimatorMove()
    {
        if (animator.applyRootMotion)
        {
            transform.parent.position += animator.deltaPosition;
        }
    }

    bool CanUpdateAnimation()
    {
        return !(playerMovement.moveState == PlayerMovement.MoveState.Rolling)
            && !(playerMovement.moveState == PlayerMovement.MoveState.Melee);
    }
}
