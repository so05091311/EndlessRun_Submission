using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵シューターのアニメーション管理
public class ShooterDie : StateMachineBehaviour
{
    public GameObject animRestartPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animRestartPos = GameObject.FindGameObjectWithTag("AnimPos");
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //アニメーションリセットポイントを過ぎたらリセットする
        if(animator.gameObject.transform.position.x < animRestartPos.transform.position.x)
        {
            animator.SetBool("isPassed", true);
        }
    }
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
