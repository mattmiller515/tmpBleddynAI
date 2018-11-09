using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bleddyn", order = 1)]
public class Bleddyn : ScriptableObject {

    [Header("General settings")]
    public float fieldOfViewAngle = 75.0f;

    [Header("Patrol settings")]
    public float patrolSpeed = 4.0f;
    public float patrolSpottingDistance = 5.0f;

    [Header("Chase settings")]
    public float chaseSpeed = 6.0f;
    public float chaseSpottingDistance = 10.0f;

    [Header("Search settings")]
    public float searchSpeed = 3.0f;
	public float seekingRadius = 6.0f;
    public float seekingTime = 15.0f;
    public float seekingSpottingDistance = 5.0f;

    [Header("Attack settings")]
    public float attackRange = 0.75f;
}
