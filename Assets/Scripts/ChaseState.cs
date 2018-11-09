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
        bleddynController.agent.speed = bleddynController.bleddynConfig.chaseSpeed;

        if (Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position) <= bleddynController.bleddynConfig.attackRange)
        {
            Debug.Log("AttackPlayer");
            bleddynController.SetTransition(Transition.ReachedPlayer);
        }
        else if (Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position) >= 10.0f)
        {
            Debug.Log("LostPlayer");
            bleddynController.SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        bleddynController.agent.destination = bleddynController.playerTransform.position;
    }
}