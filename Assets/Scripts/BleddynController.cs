using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BleddynController : AdvancedFSM 
{
    public Transform waypointsParent;

    private Transform playerTransform;

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        ConstructFSM();
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform);
    }

    public void SetTransition(Transition t) 
    { 
        PerformTransition(t); 
    }

    private void ConstructFSM()
    {
        PatrolState patrol = new PatrolState(waypointsParent, GetComponent<NavMeshAgent>());
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);

        AddFSMState(patrol);
    }
}
