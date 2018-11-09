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
        animator = bleddynController.GetComponent<Animator>();
        agent = bleddynController.agent;
    }


    public override void Reason(BleddynController bleddynController)
    {
        if (Vector3.Distance(bleddynController.playerTransform.position, bleddynController.transform.position) > bleddynController.bleddynConfig.attackRange)
        {
            bleddynController.SetTransition(Transition.SawPlayer);
            animator.SetBool("isAttacking", false);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        Debug.Log("--------------------ATTACK-------------------");
        agent.velocity = Vector3.zero;
        bleddynController.transform.LookAt(bleddynController.playerTransform);
        Debug.Log(animator.GetBool("isAttacking"));
        animator.SetBool("isAttacking", true);
    }
}
