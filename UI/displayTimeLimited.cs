using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayTimeLimited : MonoBehaviour {

	Text displayText;

	[SerializeField]
	float totalTimeDisplayed;
	float timeInteraction;

	[SerializeField]
	float totalSeedDisplayed;
	float timeSeed;
	bool seedDisplay = false;
	

	private GameController gameController;
	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		timeInteraction = totalTimeDisplayed;
	}
	
	// Update is called once per frame
	void Update () {
		updateDisplayInteraction();
		updateSeedDisplay();
	}
		
	void updateDisplayInteraction(){

		if (timeInteraction <= 0) {
			GetComponent<Text>().text = "";
		} else {
			timeInteraction -= Time.deltaTime;
		}
	}	

	void updateSeedDisplay() {
		if (seedDisplay) {
			if (timeSeed <= 0) {
				if (timeInteraction <= 0 ) {
					GetComponent<Text>().text = "";
				} else {
					GetComponent<Text>().text = "Press E to interact with objects";	
				}
				seedDisplay = false;
			} else {
				timeSeed -= Time.deltaTime;
			}
		}
	}

	void OnEnable() {
		CollectOnContact.onGainingSeed += textPlantSeed;
	}

	void OnDisable() {
		CollectOnContact.onGainingSeed -= textPlantSeed;
	}

	void textPlantSeed (){
		GetComponent<Text>().text = "Press R to plant";
		timeSeed = totalSeedDisplayed;
		seedDisplay = true;
	}
}
