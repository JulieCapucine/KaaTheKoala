using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	StateManager stateManager;
	Text timer;
	Image bg;

	Color purple;

	// Use this for initialization
	void Start () {
		purple = new Color(0.2235f, 0.1216f, 0.2235f);
		stateManager = Tools.loadStateManager();
		timer = GetComponentInChildren<Text>();
		bg =  GetComponentInChildren<Image>();
		if (Tools.getState() == State.Awake) {
			timer.enabled = false;
			bg.enabled = false;
		} else {
			timer.enabled = true;
			bg.enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (stateManager.getTime().x < 10) {
			if (stateManager.getTime().x <= 5) {
				timer.color = Color.red;
			} else {
				timer.color = purple;
			}
			timer.text = "00:0" + Mathf.Floor(stateManager.getTime().x).ToString();
		} else {
			timer.color = purple;
			timer.text = "00:" + Mathf.Floor(stateManager.getTime().x).ToString();
		}
		
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		if (Tools.getState() == State.Awake) {
			timer.enabled = false;
			bg.enabled = false;
		 } else {
		 	timer.enabled = true;
			bg.enabled = true;
		 }
		
	}
}
