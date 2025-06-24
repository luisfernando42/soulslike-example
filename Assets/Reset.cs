using UnityEngine;

public class Reset : StateMachineBehaviour
{
    
     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        InputManager inputManager = animator.GetComponentInParent<InputManager>();
        animator.SetBool(AnimationKeys.INTERACTING, false);
        inputManager.SetIsRolling(false);
    }
}
