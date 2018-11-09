using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : FSMState
{
    Animator animator;
    NavMeshAgent agent;

    public AttackState(Transform npc)
    {
        stateID = FSMStateID.Attacking;
        animator = npc.GetComponent<Animator>();
        agent = npc.GetComponent<NavMeshAgent>();
    }


    public override void Reason(Transform player, Transform npc)
    {
        if (Vector3.Distance(player.position, npc.position) > 5)
        {
            npc.GetComponent<BleddynController>().SetTransition(Transition.SawPlayer);
            animator.SetBool("isAttacking", false);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        agent.velocity = Vector3.zero;
        npc.LookAt(player);
        Debug.Log(animator.GetBool("isAttacking"));
        animator.SetBool("isAttacking", true);
    }
}
