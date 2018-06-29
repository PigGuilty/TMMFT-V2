using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMultiClient : MonoBehaviour {
	
	public string SceneToLoad = "Main";
	public Button button;
	public Text IPHolder;
	public Text info;
	
	void Start() {
		DontDestroyOnLoad(info);
		Button btnComponent = button.GetComponent<Button>();
		
		btnComponent.onClick.AddListener(OnClick);
	}
	
	void OnClick(){
		string ip = IPHolder.text;
		if(string.IsNullOrEmpty(ip)){
			ip = "localhost";
		}
		
		info.text = "Client:"+ip;
		SceneManager.LoadScene(SceneToLoad);
	}
}
