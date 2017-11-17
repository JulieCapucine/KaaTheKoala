using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerFill : MonoBehaviour {

	private StateManager stateManager;
	public Image timer;

	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		timer.fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		timer.fillAmount = stateManager.getTime().x / stateManager.getTime().y;
	}
}
