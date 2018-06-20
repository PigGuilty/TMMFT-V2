using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Network : NetworkManager {
	
	public NetworkStartPosition[] spawnPoints;
	
	void Start () {
		GameObject infoGameObject = GameObject.FindWithTag("INFO");
		if(infoGameObject != null) {
			Text info = infoGameObject.GetComponent<Text>();
			if(info.text == "Server"){
				StartHost();
			}
		}else{
			StartHost();
		}
	}
	
	void Update () {
		
	}
}
