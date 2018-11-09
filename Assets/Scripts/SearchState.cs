using System;
using UnityEngine;
using UnityEngine.AI;

public class SearchState : FSMState
{
    private NavMeshAgent agent;
    private float timer;

    public SearchState(BleddynController bleddynController)
    {
        stateID = FSMStateID.Searching;
        this.agent = bleddynController.agent;
        this.timer = bleddynController.bleddynConfig.seekingTime;
    }

    public override void Reason(BleddynController bleddynController)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Debug.Log("GiveUpSearching");
            timer = bleddynController.bleddynConfig.seekingTime;
            bleddynController.SetTransition(Transition.GiveUpSearching);

            moveToClosestPoint(bleddynController);
        }

        if (Vector3.Distance(bleddynController.transform.position, bleddynController.playerTransform.position) <= bleddynController.bleddynConfig.seekingSpottingDistance)
        {
            Debug.Log("SawPlayer");
            bleddynController.SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(BleddynController bleddynController)
    {
        bleddynController.agent.speed = bleddynController.bleddynConfig.searchSpeed;

        Vector3 lastKnownPlayerPosition = bleddynController.lastKnownPlayerPosition;

        if (Vector3.Distance(agent.destination, bleddynController.transform.position) < 1)
        {
            Vector3 randomSearchPosition = new Vector3(
                lastKnownPlayerPosition.x + UnityEngine.Random.Range(-bleddynController.bleddynConfig.seekingRadius, bleddynController.bleddynConfig.seekingRadius),
                lastKnownPlayerPosition.y,
                lastKnownPlayerPosition.z + UnityEngine.Random.Range(-bleddynController.bleddynConfig.seekingRadius, bleddynController.bleddynConfig.seekingRadius)
            );
            agent.destination = randomSearchPosition;
        }
    }

    private void moveToClosestPoint(BleddynController bleddynController)
    {
        Transform curNavPoint = bleddynController.allWaypoints[0];
        Vector3 alignedAgentPosition = new Vector3(agent.transform.position.x, curNavPoint.position.y, agent.transform.position.z);

        for (int i=0; i < bleddynController.allWaypoints.Length; i++)
        {
            if (Vector3.Distance(alignedAgentPosition, bleddynController.allWaypoints[i].position) < Vector3.Distance(alignedAgentPosition, curNavPoint.position))
            {
                curNavPoint = bleddynController.allWaypoints[i];
            }
        }

        agent.destination = curNavPoint.position;
        bleddynController.patrolIndex = Array.IndexOf(bleddynController.allWaypoints, curNavPoint);
    }
}
