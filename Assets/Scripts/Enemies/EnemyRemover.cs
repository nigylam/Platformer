using UnityEngine;

public class EnemyRemover : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
