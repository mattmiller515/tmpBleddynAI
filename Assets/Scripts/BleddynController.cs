using UnityEngine;
using System.Collections;

public class BleddynController : AdvancedFSM 
{
    private Transform playerTransform;

    //Initialize the Finite state machine for the NPC tank
    protected override void Initialize()
    {
        //Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        //Start Doing the Finite State Machine
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
        Transform[] waypoints = null;

        PatrolState patrol = new PatrolState(waypoints);
        patrol.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);

        AddFSMState(patrol);
    }
}
