using UnityEngine;using System.Collections;using UnityEngine.AI;

public class ChaseState : FSMState{    public ChaseState()
    {        stateID = FSMStateID.Chasing;    }    public override void Reason(Transform player, Transform npc)    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        //Check the distance with player tank
        //When the distance is near, transition to chase state
        if (Vector3.Distance(npc.position, player.position) >= 5.0f)        {            Debug.Log("Lost Player");            npc.GetComponent<BleddynController>().SetTransition(Transition.LostPlayer);        }    }    public override void Act(Transform player, Transform npc)    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        var distance = Vector3.Distance(npc.position, player.position);
        Debug.Log("chasing, Distance: " + distance);
    }}