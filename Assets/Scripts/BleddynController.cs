using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BleddynController : AdvancedFSM 
{
    public Bleddyn bleddynConfig;
    public Transform waypointsParent;
    public Transform[] allWaypoints;

    [HideInInspector]
    public Vector3 lastKnownPlayerPosition;

    public Transform playerTransform;
    public NavMeshAgent agent;
    public int patrolIndex;

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        agent = GetComponent<NavMeshAgent>();

        ConstructFSM();
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(this);
        CurrentState.Act(this);
    }

    public void SetTransition(Transition t) 
    { 
        PerformTransition(t); 
    }

    private void ConstructFSM()
    {
        FSMState patrol = new PatrolState(this);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        AddFSMState(patrol);

        FSMState chase = new ChaseState();
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Searching);
        AddFSMState(chase);

        FSMState search = new SearchState(this);
        search.AddTransition(Transition.GiveUpSearching, FSMStateID.Patrolling);
        search.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        AddFSMState(search);
    }
}
