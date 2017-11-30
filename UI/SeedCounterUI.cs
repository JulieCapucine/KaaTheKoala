using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedCounterUI : MonoBehaviour {

	[SerializeField]
	Sprite inventoryFull;
	[SerializeField]
	Sprite inventoryEmpty;

	Text seedCounter;
	GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		seedCounter = GetComponentInChildren<Text>();
		seedCounter.enabled = false;
	}

	void OnEnable() {
		ObjectController.onGainingSeed += collectSeed;
		GameController.onPlanting += plantSeed;
	}

	void OnDisable() {
		ObjectController.onGainingSeed -= collectSeed;
		GameController.onPlanting -= plantSeed;
	}

	void collectSeed (){
		seedCounter.enabled = true;
		seedCounter.text = gameController.getSeedCounter().ToString();
		GetComponentInChildren<Image>().sprite = inventoryFull;
	}

	void plantSeed() {
		if (gameController.getSeedCounter() == 0) {
			seedCounter.enabled = false;
			GetComponentInChildren<Image>().sprite = inventoryEmpty;
		} else {
			seedCounter.text = gameController.getSeedCounter().ToString();
		}
	}

}

