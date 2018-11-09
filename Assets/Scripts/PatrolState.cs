<<<<<<< HEAD
using UnityEngine;using System.Collections;using System.Collections.Generic;
using UnityEngine.AI;

public class PatrolState : FSMState{    private List<Transform> patrolPoints;    private int patrolIndex;    private NavMeshAgent agent;    public PatrolState(Transform waypointsParent, NavMeshAgent agent)     {        stateID = FSMStateID.Patrolling;        this.agent = agent;        patrolPoints = new List<Transform>();        for (int i=0; i < waypointsParent.childCount; i++)
        {
            patrolPoints.Add(waypointsParent.GetChild(i).transform);
        }        patrolIndex = 0;    }    public override void Reason(Transform player, Transform npc)    {        if (Vector3.Distance(npc.position, player.position) <= 5.0f)        {            npc.GetComponent<BleddynController>().SetTransition(Transition.ReachedPlayer);        }    }    public override void Act(Transform player, Transform npc)    {        Vector3 curNavPoint = patrolPoints[patrolIndex].position;        agent.destination = curNavPoint;        Vector3 alignedAgentPosition = new Vector3(agent.transform.position.x, curNavPoint.y, agent.transform.position.z);        if (Vector3.Distance(curNavPoint, alignedAgentPosition) <= 1)
        {
            if (++patrolIndex >= patrolPoints.Count)
            {
                patrolIndex = 0;
            }
        }    }}
=======
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class PatrolState : FSMState
{
    private List<Transform> patrolPoints;
    private int patrolIndex;
    private NavMeshAgent agent;

    public PatrolState(Transform waypointsParent, NavMeshAgent agent)
    {
        stateID = FSMStateID.Patrolling;
        this.agent = agent;

        patrolPoints = new List<Transform>();
        for (int i = 0; i < waypointsParent.childCount; i++)
        {
            patrolPoints.Add(waypointsParent.GetChild(i).transform);
        }
        patrolIndex = 0;
    }

    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(npc.position, player.position) <= 5.0f)
        {
            npc.GetComponent<BleddynController>().SetTransition(Transition.SawPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        Vector3 curNavPoint = patrolPoints[patrolIndex].position;
        agent.destination = curNavPoint;

        Vector3 alignedAgentPosition = new Vector3(agent.transform.position.x, curNavPoint.y, agent.transform.position.z);
        if (Vector3.Distance(curNavPoint, alignedAgentPosition) <= 1)
        {
            if (++patrolIndex >= patrolPoints.Count)
            {
                patrolIndex = 0;
            }
        }
    }
}
>>>>>>> c89529ea03a179383f72008265b5e10a1ef48026
