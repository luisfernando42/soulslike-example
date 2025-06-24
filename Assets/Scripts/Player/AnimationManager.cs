using UnityEngine;
using UnityEngine.Windows;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;
    private int verticalId;
    private int horizontalId;
    private bool canRotate = true;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        verticalId = Animator.StringToHash(AnimationKeys.VERTICAL);
        horizontalId = Animator.StringToHash(AnimationKeys.HORIZONTAL);
    }

    public void UpdateValues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        float vertical = 0;
        if (verticalMovement > 0)
            vertical = verticalMovement < 0.5f ? 0.5f : 1f;
        else if (verticalMovement < 0)
            vertical = verticalMovement > -0.5f ? -0.5f : -1f;
        else
            vertical = 0;

        float horizontal = 0;
        if (horizontalMovement > 0)
            horizontal = horizontalMovement < 0.5f ? 0.5f : 1f;
        else if (horizontalMovement < 0)
            horizontal = horizontalMovement > -0.5f ? -0.5f : -1f;
        else
            horizontal = 0;

        if(isSprinting)
        {
            vertical = 2;
            horizontal = horizontalMovement;
        }

        animator.SetFloat(verticalId, vertical, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontalId, horizontal, 0.1f, Time.deltaTime);
    }

    public void playAnimation(string animation, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool(AnimationKeys.INTERACTING, isInteracting);
        animator.CrossFade(animation, 0.2f);
    }

    private void OnAnimatorMove()
    {
        if (!animator.GetBool(AnimationKeys.INTERACTING)) return;

        player.getPlayerRigidBody().linearDamping = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / Time.deltaTime;
        player.getPlayerRigidBody().linearVelocity = velocity;
    }


    public void setCanRotate(bool value)
    {
        canRotate = value;
    }

    public bool getCanRotate()
    {
        return canRotate;
    }

    public bool getIsInteracting()
    {
        return animator.GetBool(AnimationKeys.INTERACTING);
    }
}
