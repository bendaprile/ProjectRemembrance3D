using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationUpdater : MonoBehaviour
{
    public bool disableMovement = false;

    [SerializeField] EnemyTemplateMaster ETM = null; //used for AnimationCalledFuncs

    [SerializeField] float transitionFade = 0.25f;
    [SerializeField] float forcetransitionFade = 0.1f;
    private string currentAnim = "idle";
    private bool canUpdateAnimation = true;

    public bool genericBool0 = false; //GENERICBOOL is set to False when an AnimationOver ends (Sustained)

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animName, bool rootMotion = false, bool force = false)
    {
        if (canUpdateAnimation || force)
        {
            float fadeVar;
            if (force)
            {
                fadeVar = forcetransitionFade;
            }
            else
            {
                fadeVar = transitionFade;
            }
            animator.applyRootMotion = rootMotion;

            if (currentAnim != animName)
            {
                animator.CrossFadeInFixedTime(animName, fadeVar);
                currentAnim = animName;
            }
        }
    }

    public void PlayUpperBodyAnimation(string animName)
    {
        animator.CrossFadeInFixedTime(animName, transitionFade, 1);
        currentAnim = animName;
    }

    public void PlayFullAnimation(string animName, bool rootMotion = false, bool disableMove = false)
    {
        animator.applyRootMotion = rootMotion;
        disableMovement = disableMove;

        animator.CrossFadeInFixedTime(animName, forcetransitionFade);
        currentAnim = animName;
        canUpdateAnimation = false;
    }

    private void OnAnimatorMove()
    {
        if (animator.applyRootMotion)
        {
            transform.parent.position += animator.deltaPosition;
        }
    }

    public void AnimationOver()
    {
        disableMovement = false;
        canUpdateAnimation = true;
        animator.applyRootMotion = false;
        genericBool0 = false;
    }

    public void Flipbool0() //GENERICBOOL is set to False when an AnimationOver ends
    {
        genericBool0 = !genericBool0;
    }

    public void AnimationCalledFunc0_inUpdater()
    {
        ETM.AnimationCalledFunc0();
    }
}
