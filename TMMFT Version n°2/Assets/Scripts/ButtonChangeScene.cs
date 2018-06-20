using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonChangeScene : MonoBehaviour {
	
	public string SceneToLoad = "Main";
	public Button button;
	
	void Start() {
		Button btnComponent = button.GetComponent<Button>();
		
		btnComponent.onClick.AddListener(OnClick);
	}
	
	void OnClick(){
		SceneManager.LoadScene(SceneToLoad);
	}
}
