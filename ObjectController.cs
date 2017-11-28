using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController: MonoBehaviour {

	Vector3 pos;
	float timeCounter;

	GameObject player;
	StateManager stateManager;

	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		player = stateManager.getPlayer();
		rb = GetComponent<Rigidbody>();
		//rb.isKinematic = true;
		GetComponents<Collider>()[0].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay() {
		if (Tools.getState() == State.Asleep) {
			if (Input.GetKeyDown (KeyCode.E)) {
				rb.isKinematic = false;
				GetComponents<Collider>()[0].enabled = true;
			} else if (Input.GetKeyUp(KeyCode.E)) {
				rb.isKinematic = true;
				GetComponents<Collider>()[0].enabled = false;
			}
		}
		
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		 if (Tools.getState() == State.Asleep) {
		 	GetComponents<Collider>()[0].enabled = false;
		 } else if (Tools.getState() == State.Awake) {
		 	GetComponents<Collider>()[0].enabled = true;
		 }
		
	}
		
}
