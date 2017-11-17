using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour {

	public bool isStartFirst;
	public bool isStartSecond;
	public bool isQuit;

	void OnMouseUp(){
		if(isStartFirst)
		{	
			SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
			//Application.LoadLevel(1);
		}
		if(isStartSecond)
		{
			SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
		}
		if (isQuit)
		{
			Application.Quit();
		}
	} 
}
