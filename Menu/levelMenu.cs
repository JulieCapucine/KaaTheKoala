using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelMenu : MonoBehaviour {

	[SerializeField]
	bool replay;
	[SerializeField]
	bool nextLvl;
	[SerializeField]
	bool menu;
	[SerializeField]
	bool quit;

	void OnMouseUp(){
		if (replay) {
			SceneManager.LoadScene(Tools.level, LoadSceneMode.Single);
		} else if (nextLvl){
			if (Tools.level == "Level1") {
				SceneManager.LoadScene("Level2", LoadSceneMode.Single);
			} else if (Tools.level == "Level2") {
				SceneManager.LoadScene("Level3", LoadSceneMode.Single);
			}
		} else if (menu) {
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
		} else if (quit) {
		 	Application.Quit();
		}
	} 

	void Start() {

	}
	
}
