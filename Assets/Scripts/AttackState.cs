using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FSMState
{
    Animator animator;
    NavMeshAgent agent;

    public AttackState(BleddynController bleddynController)
    {
        stateID = FSMStateID.Attacking;
        animator = bleddynController.animator;
        agent = bleddynController.agent;
    }


    public override void Reason(BleddynController bleddynController)
    {
        if (Vector3.Distance(bleddynController.playerTransform.position, bleddynController.transform.position) > bleddynController.bleddynConfig.attackRange)
        {
            Debug.Log(Vector3.Distance(bleddynController.playerTransform.position, bleddynController.transform.position));
            bleddynController.SetTransition(Transition.SawPlayer);
            animator.SetBool("isAttacking", false);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        agent.velocity = Vector3.zero;
        animator.SetBool("isAttacking", true);
    }
}
