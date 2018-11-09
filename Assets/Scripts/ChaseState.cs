using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    public ChaseState()
    {
        stateID = FSMStateID.Chasing;
    }

    public override void Reason(Transform player, Transform npc)
    {
        bool canSeePlayer = npc.GetComponent<FieldOfView>().CanSeePlayer();
        if (!canSeePlayer)
        {
            Debug.Log("Lost Player");
            npc.GetComponent<BleddynController>().SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        agent.destination = player.position;
        var distance = Vector3.Distance(npc.position, player.position);
        Debug.Log("chasing, Distance: " + distance);
    }
}