using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    public ChaseState()
    {
        stateID = FSMStateID.Chasing;
    }

    public override void Reason(BleddynController bleddynController)
    {
        if (bleddynController.playerInFOV())
        {
            float distanceToPlayer = Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position);

            if (distanceToPlayer < bleddynController.bleddynConfig.attackRange)
            {
                Debug.Log("AttackPlayer");
                bleddynController.SetTransition(Transition.ReachedPlayer);
            }

            if (distanceToPlayer > bleddynController.bleddynConfig.chaseSpottingDistance)
            {
                Debug.Log("LostPlayer");
                bleddynController.lastKnownPlayerPosition = bleddynController.playerTransform.position;
                bleddynController.SetTransition(Transition.LostPlayer);
            }
        }
        else
        {
            Debug.Log("PlayerOutOfSight");
            bleddynController.lastKnownPlayerPosition = bleddynController.playerTransform.position;
            bleddynController.SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        bleddynController.agent.speed = bleddynController.bleddynConfig.chaseSpeed;
        bleddynController.agent.destination = bleddynController.playerTransform.position;
    }
}