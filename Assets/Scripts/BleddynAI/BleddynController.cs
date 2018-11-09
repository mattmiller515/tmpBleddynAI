using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BleddynController : AdvancedFSM 
{
    public Bleddyn bleddynConfig;
    public Transform waypointsParent;

    [HideInInspector]
    public Transform[] allWaypoints;

    [HideInInspector]
    public Vector3 lastKnownPlayerPosition;

    [HideInInspector]
    public Transform playerTransform;

    [HideInInspector]
    public NavMeshAgent agent;

    [HideInInspector]
    public int patrolIndex;

    [HideInInspector]
    public Animator animator;

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        lastKnownPlayerPosition = playerTransform.position;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ConstructFSM();
    }

    protected override void FSMLateUpdate()
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
        patrol.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        AddFSMState(patrol);

        FSMState chase = new ChaseState();
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Searching);
        chase.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        AddFSMState(chase);

        FSMState search = new SearchState(this);
        search.AddTransition(Transition.GiveUpSearching, FSMStateID.Patrolling);
        search.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        AddFSMState(search);

        FSMState attack = new AttackState(this);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Searching);
        AddFSMState(attack);
    }

    public bool playerInFOV()
    {
        Vector3 targetDir = playerTransform.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDir, transform.forward);

        return angleToPlayer >= -bleddynConfig.fieldOfViewAngle && angleToPlayer <= bleddynConfig.fieldOfViewAngle;
    }
}
