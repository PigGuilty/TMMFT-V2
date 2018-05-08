using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VacheScript : MonoBehaviour {

	private GameObject goal;
	private NavMeshAgent agent;

    void Start () {
		agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindWithTag("Player");
    }

	void Update() {
		agent.destination = goal.transform.position;
	}
}	
