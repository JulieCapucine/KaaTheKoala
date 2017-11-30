using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayText : MonoBehaviour {

	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = false;

	}

	void OnTriggerEnter(Collider other) {
		if (Tools.getState() == State.Awake) {
			if (other.tag == "Player") {
				rend.enabled = true;
			}
		}
	}


	void OnTriggerStay(Collider other) {
		if (Tools.getState() == State.Awake) {
			if (other.tag == "Player") {
				rend.enabled = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (Tools.getState() == State.Awake) {
			if (other.tag == "Player") {
				rend.enabled = false;
			}
		}
	}

}
