using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	public GameObject[] spawns;
	
	private float SpawnCount;
	private int[] probas;
	private int previousScore;
	private float Counter;
	
	void Start() {
		probas = new int[]{ 100, 0, 0 };
		
		previousScore = 0;
		Counter = 2;
		SpawnCount = 0;
		
		foreach(GameObject spawn in spawns){
			spawn.GetComponent<Spawner>().probas = probas;
		}
	}
	
	void Update () {
		Counter -= Time.deltaTime;
		
		if (Counter <= 0) {
			int score = getScore();
			int nbJoueurs = getNbJoueurs();
			
			Counter = Frequence(score, nbJoueurs);
			
			int toAdd = score - previousScore;
			
			if(score > previousScore){
				if(probas[1] < 33){
					probas[1] += toAdd;
					probas[0] -= toAdd;
				}else if(probas[2] < 33 && probas [1] == 33){
					probas[2] += toAdd;
					probas[0] -= toAdd;
				}
				
				if(probas[1] > 33){
					probas[1] = 33;
				}
				
				if(probas[2] > 33){
					probas[0] = 34;
					probas[1] = 33;
					probas[2] = 33;
				}
			}
			
			foreach(GameObject spawn in spawns){
				spawn.GetComponent<Spawner>().probas = probas;
			}
			print(probas[0] +" " + probas[1] + " "+ probas[2]);
			
			int index = Random.Range(0,spawns.Length);
			int maxLoop = spawns.Length;
			int turn = 0;
			while(spawns[index].GetComponent<Spawner>().isEntered){
				index = Random.Range(0,spawns.Length);
				
				turn ++;
				if(turn > maxLoop)
					break;
			}
			
			spawns[index].GetComponent<Spawner>().Call();
			
			previousScore = score;
		}
	}
	
	int getScore(){
		GameObject ScoreTextObject = GameObject.FindWithTag ("localScore");
		if(ScoreTextObject != null){
			Text ScoreText = ScoreTextObject.GetComponent<Text> ();
			string scoreEnText = ScoreText.text.Replace ("Score : ", "");
			
			return int.Parse (scoreEnText);
		}else{
			return previousScore;
		}
	}
	
	int getNbJoueurs(){
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		
		if(players != null)
			return players.Length + 1;
		else
			return 1;
	}
	
	float Frequence(int score, int nbJoueurs){
		return (300*nbJoueurs)/(score+60*nbJoueurs);
	}
}