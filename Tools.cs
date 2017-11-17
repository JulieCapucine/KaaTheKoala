using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public static class Tools
 {

 	//returns the GameController of the game
   public static GameController loadGameController() {
   		GameController gameController = null;
     	GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		return gameController;
   	}

   	//returns the stateManager of the Game, which contains the active player
    public static StateManager loadStateManager() {

    	StateManager stateManager = null;

   		GameObject stateManagerObject = GameObject.FindWithTag ("StateManager");
		if (stateManagerObject != null)
		{
			stateManager = stateManagerObject.GetComponent <StateManager>();
		}
		if (stateManager == null)
		{
			Debug.Log ("Cannot find 'StateManager' script");
		}
		return stateManager;
	}

 }