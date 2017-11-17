using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFill : MonoBehaviour {

	private StateManager stateManager;
	public Image healthBar;


	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		healthBar.fillAmount = 1;
	}

	// Update is called once per frame
	void Update () {
		float conv = stateManager.getHealth().x / stateManager.getHealth().y;
		healthBar.fillAmount = conv;
	}

}
