using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{

    public float mouseAngle;
    float movingAngle;

    private void Start()
    {
        mouseAngle = 0f;
        movingAngle = 0f;
    }

    private void FixedUpdate()
    {
        CalculateMouseDirection();
        CalculateMovingDirection();
    }

    public enum MoveDir
    {
        Forward,
        Left,
        Right,
        Backward
    }

    /*
     * Returns the angle of the mouse from -180 to 180
     */
    public void CalculateMouseDirection()
    {
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        v3Pos = Input.mousePosition - v3Pos;
        float angle = -(Vector2.SignedAngle(Vector2.up, v3Pos));
        if (angle < 0)
        {
            angle += 360f;
        }
        mouseAngle = angle;
    }

    public void CalculateMovingDirection()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 normDir = direction.normalized;
        float angle = -(Vector2.SignedAngle(Vector2.up, normDir));
        if (angle < 0)
        {
            angle += 360f;
        }
        movingAngle = angle;
    }

    public MoveDir GetMoveDirection()
    {
        MoveDir moveDirection = MoveDir.Forward;

        float angleDifferenceAbs = Mathf.Abs(mouseAngle - movingAngle);
        float angleDifference = mouseAngle - movingAngle;

        if (angleDifferenceAbs < 45 || angleDifferenceAbs > 315f)
        {
            moveDirection = MoveDir.Forward;
        }
        else if (angleDifferenceAbs < 135f || angleDifferenceAbs > 225f)
        {

            if (angleDifference > 180f || (angleDifference <= 0f && angleDifference >= -180f))
            {
                moveDirection = MoveDir.Right;
            }
            else if (angleDifference < -180f || (angleDifference > 0f && angleDifference <= 180f))
            {
                moveDirection = MoveDir.Left;
            }
            else
            {
                Debug.LogError("Move Direction is Broken");
            }
        }
        else
        {
            moveDirection = MoveDir.Backward;
        }

        return moveDirection;
    }

    public float AngleToDirection()
    {
        float diff_angle = mouseAngle - movingAngle;
        if (diff_angle > 180)
        {
            diff_angle -= 360;
        }
        else if (diff_angle < -180)
        {
            diff_angle += 360;
        }

        float abs_angle = Mathf.Abs(diff_angle);
        float bias;


        if (abs_angle < 45)
        {
            bias = 0f;
        }
        else if (abs_angle < 135)
        {
            bias = 90f * Mathf.Sign(diff_angle);
        }
        else
        {
            bias = 180f;

        }
        return bias + movingAngle + 45;
    }
}
