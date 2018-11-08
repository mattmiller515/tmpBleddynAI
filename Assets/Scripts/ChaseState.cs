﻿using UnityEngine;

public class ChaseState : FSMState
    {
        waypoints = wp;
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        //Check the distance with player tank
        //When the distance is near, transition to chase state
        if (Vector3.Distance(npc.position, player.position) >= 50.0f)
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.destination = player.position;
        var distance = Vector3.Distance(npc.position, player.position);
        Debug.Log("chasing, Distance: " + distance);
    }