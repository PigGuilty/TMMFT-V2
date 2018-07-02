using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class VacheScript : MonoBehaviour {

	private GameObject goal;
	private NavMeshAgent agent;
	private List<NavMeshPath> paths;
    private List<GameObject> players;
	private GameObject player;
	private float elapsed = 1.0f; 
	private float clock = 1.0f;
	
	private bool m_isServer;
	
    void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = true;
        player = GameObject.FindWithTag("localPlayer");
		if(player != null){
			m_isServer = player.GetComponent<FirstPersonController>().isServer;
		}
		
		players = new List<GameObject>();
		paths = new List<NavMeshPath>();
		
		GameObject[] tmpPlayers = GameObject.FindGameObjectsWithTag("Player");
		if(tmpPlayers != null)
			players.AddRange(tmpPlayers);
		players.Add(player);
    }

	void Update() {
		if(player != null && m_isServer){
			elapsed += Time.deltaTime;
			
			if (elapsed > clock || agent.path == null) {
				elapsed -= clock;
				
				paths.Clear();
				foreach(GameObject p in players) {
					NavMeshPath path = new NavMeshPath();
					NavMesh.CalculatePath(transform.position, p.transform.position, NavMesh.AllAreas, path);
					
					paths.Add(path);
				}
				
				NavMeshPath shortestPath = paths[0];
				float lastLength = float.PositiveInfinity;
				
				foreach(NavMeshPath path in paths) {
					if(path.status == NavMeshPathStatus.PathComplete){
						agent.SetPath(path);
						shortestPath = Math.Min(lastLength, agent.remainingDistance) == lastLength ? shortestPath : path;
					}
				}
				
				goal = players[paths.IndexOf(shortestPath)];
			}
			
			agent.destination = goal.transform.position;
		}else{
			GameObject player = GameObject.FindWithTag("localPlayer");
			if(player != null){
				m_isServer = player.GetComponent<FirstPersonController>().isServer;
			}
		}
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
		
	void OnPlayerConnected(NetworkPlayer np){
		players.Clear();
		
		GameObject[] tmpPlayers = GameObject.FindGameObjectsWithTag("Player");
		if(tmpPlayers != null)
			players.AddRange(tmpPlayers);
		players.Add(player);
	}
	
	void OnPlayerDisconnected(NetworkPlayer np){
		players.Clear();
		
		GameObject[] tmpPlayers = GameObject.FindGameObjectsWithTag("Player");
		if(tmpPlayers != null)
			players.AddRange(tmpPlayers);
		players.Add(player);
	}
}	
