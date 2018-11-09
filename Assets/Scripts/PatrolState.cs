using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class PatrolState : FSMState
{
    public PatrolState(BleddynController bleddynController) 
    {
        stateID = FSMStateID.Patrolling;

        bleddynController.allWaypoints = new Transform[bleddynController.waypointsParent.childCount];
        for (int i=0; i < bleddynController.waypointsParent.childCount; i++)
        {
            bleddynController.allWaypoints[i] = (bleddynController.waypointsParent.GetChild(i).transform);
        }
        bleddynController.patrolIndex = 0;
    }

    public override void Reason(BleddynController bleddynController)
    {
        if (Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position) <= bleddynController.bleddynConfig.attackRange)
        {
            Debug.Log("AttackPlayer");
            bleddynController.SetTransition(Transition.ReachedPlayer);
        }
        else if (Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position) <= bleddynController.bleddynConfig.sightDistance)
        {
            Debug.Log("SawPlayer");
            bleddynController.SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        Vector3 curNavPoint = bleddynController.allWaypoints[bleddynController.patrolIndex].position;
        bleddynController.agent.destination = curNavPoint;
        bleddynController.agent.speed = bleddynController.bleddynConfig.patrolSpeed;

        Vector3 alignedAgentPosition = new Vector3(bleddynController.agent.transform.position.x, curNavPoint.y, bleddynController.agent.transform.position.z);
        if (Vector3.Distance(curNavPoint, alignedAgentPosition) <= 1)
        {
            if (++bleddynController.patrolIndex >= bleddynController.allWaypoints.Length)
            {
                bleddynController.patrolIndex = 0;
            }
        }
    }
}