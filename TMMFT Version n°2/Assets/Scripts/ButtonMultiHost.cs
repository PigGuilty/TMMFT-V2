using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonMultiHost : MonoBehaviour {
	
	public string SceneToLoad = "Main";
	public Button button;
	public Text info;
	
	void Start() {
		DontDestroyOnLoad(info);
		Button btnComponent = button.GetComponent<Button>();
		
		btnComponent.onClick.AddListener(OnClick);
	}
	
	void OnClick(){
		info.text = "Server";
		SceneManager.LoadScene(SceneToLoad);
	}
}
