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

	
	Image imgComponent;
	[SerializeField]
	Sprite plantSeed;

	// Use this for initialization
	void Start () {
		imgComponent = GameObject.Find("InstructionText").GetComponent<Image>();
		timeInteraction = totalTimeDisplayed;
	}
	
	// Update is called once per frame
	void Update () {
		if (Tools.getState() == State.Awake) {
			updateSeedDisplay();
		} else {
			imgComponent.enabled = false;
		}
	}

	void updateDisplayMovement() {
		if (timeInteraction <= 0 ) {
			GetComponent<Text>().text = "";
		} else {
			GetComponent<Text>().text = "Press E to move objects";
			timeInteraction -= Time.deltaTime;	
		}

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
				imgComponent.enabled = false;
				seedDisplay = false;
			} else {

				imgComponent.enabled = true;
				timeSeed -= Time.deltaTime;
			}
		}
	}

	void OnEnable() {
		ObjectController.onGainingSeed += textPlantSeed;
	}

	void OnDisable() {
		ObjectController.onGainingSeed -= textPlantSeed;
	}

	void textPlantSeed (){
		imgComponent.enabled = true;
		imgComponent.sprite = plantSeed;
		seedDisplay = true;
		timeSeed = totalSeedDisplayed;
	}
}
