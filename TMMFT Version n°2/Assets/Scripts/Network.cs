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
			}else if(info.text.StartsWith("Client")){
				string alpha = info.text;
				networkAddress = alpha.Split(new char[]{':'})[1];
				networkPort = 7777;
				StartClient();
			}
			
		}else{ // If started directly from Game scene or info text missing, be host
			StartHost();
		}
	}
}
