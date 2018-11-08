using UnityEngine;using System.Collections;using UnityEngine.AI;

public class ChaseState : FSMState{    public ChaseState(Transform[] wp)
    {
        waypoints = wp;        stateID = FSMStateID.Chasing;        curRotSpeed = 1.0f;        curSpeed = 100.0f;    }    public override void Reason(Transform player, Transform npc)    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        //Check the distance with player tank
        //When the distance is near, transition to chase state
        if (Vector3.Distance(npc.position, player.position) >= 50.0f)        {            Debug.Log("Lost Player");            agent.isStopped = true;            npc.GetComponent<BleddynController>().SetTransition(Transition.LostPlayer);        }    }    public override void Act(Transform player, Transform npc)    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.destination = player.position;
        var distance = Vector3.Distance(npc.position, player.position);
        Debug.Log("chasing, Distance: " + distance);
    }}