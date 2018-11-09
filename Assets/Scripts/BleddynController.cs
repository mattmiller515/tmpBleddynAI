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
    public Animator animator;
    public int patrolIndex;

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
        patrol.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        AddFSMState(patrol);

        FSMState chase = new ChaseState();
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Searching);
        chase.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        AddFSMState(chase);

        FSMState search = new SearchState(this);
        search.AddTransition(Transition.GiveUpSearching, FSMStateID.Patrolling);
        search.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        search.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);
        AddFSMState(search);

        FSMState attack = new AttackState(this);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        AddFSMState(attack);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("__________collision__________");
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
