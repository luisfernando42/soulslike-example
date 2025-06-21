using UnityEngine;
using UnityEngine.Windows;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private int verticalId;
    private int horizontalId;
    private bool canRotate = true;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        verticalId = Animator.StringToHash(AnimationKeys.VERTICAL);
        horizontalId = Animator.StringToHash(AnimationKeys.HORIZONTAL);
    }

    public void UpdateValues(float verticalMovement, float horizontalMovement)
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

        animator.SetFloat(verticalId, vertical, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontalId, horizontal, 0.1f, Time.deltaTime);
    }

    public void setCanRotate(bool value)
    {
        canRotate = value;
    }

    public bool getCanRotate()
    {
        return canRotate;
    }
}
