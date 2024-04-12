using UnityEngine;

public class RemoveEnemy : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
