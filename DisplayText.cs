using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour {

	private Renderer rend;
	private StateManager stateManager;



	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		rend = GetComponent<Renderer>();
		rend.enabled = false;

	}

	void OnTriggerEnter(Collider other) {
		if (stateManager.getState() == State.Awake) {
			if (other.tag == "Player") {
				rend.enabled = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (stateManager.getState() == State.Awake) {
			if (other.tag == "Player") {
				rend.enabled = false;
			}
		}
	}

}
