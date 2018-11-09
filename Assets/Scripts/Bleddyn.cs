using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bleddyn", order = 1)]
public class Bleddyn : ScriptableObject {

    [Header("Patrol settings")]
    public float patrolSpeed;
    public float patrolSpottingDistance;

    [Header("Chase settings")]
    public float chaseSpeed;
    public float chaseSpottingDistance;

    [Header("Search settings")]
    public float searchSpeed;
	public float seekingRadius;
    public float seekingTime;
    public float seekingSpottingDistance;

    [Header("Attack settings")]
    public float attackRange;
}
