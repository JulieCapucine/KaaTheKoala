using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedCounterUI : MonoBehaviour {

	public Text seedNb;
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		seedNb.text = gameController.getSeedCounter().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		seedNb.text = gameController.getSeedCounter().ToString();
	}
}
