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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Escalier +x")
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (other.gameObject.tag == "Escalier -x")
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (other.gameObject.tag == "Escalier +y")
        {
            gameObject.transform.position += new Vector3(0, 0, 0.1f);
        }
        if (other.gameObject.tag == "Escalier -y")
        {
            gameObject.transform.position += new Vector3(0, 0, -0.1f);
        }
    }
}	
