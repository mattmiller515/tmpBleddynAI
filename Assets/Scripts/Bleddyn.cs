using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Bleddyn", order = 1)]
public class Bleddyn : ScriptableObject {

    public float patrolSpeed;
    public float chaseSpeed;
    public float searchSpeed;
	public float seekingDistance;
    public float seekingTime;
    public float sightDistance;
    public float attackRange;
}
