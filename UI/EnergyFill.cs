using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyFill : MonoBehaviour {

	private StateManager stateManager;
	public Image energyBar;


	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		energyBar.fillAmount = 1;
		//stateManager = stateManager.FindWithTag ("StateManager");
	}
	
	// Update is called once per frame
	void Update () {
		float conv = (stateManager.getDistanceMax() - stateManager.getDistanceTravelled()) / stateManager.getDistanceMax();
		energyBar.fillAmount = conv;
	}


}
