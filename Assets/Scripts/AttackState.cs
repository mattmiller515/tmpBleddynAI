using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FSMState
{
    NavMeshAgent agent;

    public AttackState(BleddynController bleddynController)
    {
        stateID = FSMStateID.Attacking;
        agent = bleddynController.agent;
    }


    public override void Reason(BleddynController bleddynController)
    {
        if (bleddynController.playerInFOV())
        {
            float distanceToPlayer = Vector3.Distance(bleddynController.playerTransform.position, bleddynController.transform.position);

            if (distanceToPlayer > bleddynController.bleddynConfig.attackRange)
            {
                if (!bleddynController.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !bleddynController.animator.IsInTransition(0))
                {
                    Debug.Log("SawPlayer");
                    bleddynController.SetTransition(Transition.SawPlayer);
                    bleddynController.animator.SetBool("isAttacking", false);
                }
            }

            if (distanceToPlayer > bleddynController.bleddynConfig.chaseSpottingDistance)
            {
                if (!bleddynController.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !bleddynController.animator.IsInTransition(0))
                {
                    Debug.Log("LostPlayer");
                    bleddynController.SetTransition(Transition.LostPlayer);
                    bleddynController.animator.SetBool("isAttacking", false);
                }
            }
        }
        else
        {
            Debug.Log("PlayerOutOfSight");
            bleddynController.SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        agent.velocity = Vector3.zero;
        bleddynController.transform.LookAt(bleddynController.playerTransform);
        bleddynController.animator.SetBool("isAttacking", true);
    }
}
