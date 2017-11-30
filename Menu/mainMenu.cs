using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour {

	public bool isStart;
	public bool isQuit;

	void OnMouseUp(){
		if(isStart)
		{	
			SceneManager.LoadScene("Level1", LoadSceneMode.Single);
		} else if(isQuit)
		{
			Application.Quit();
		}
	} 
}
