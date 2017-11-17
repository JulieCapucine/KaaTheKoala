using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

	bool isMoving;
	Vector3 pos;
	float timeCounter;

	GameObject player;
	StateManager stateManager;
	
	// Use this for initialization
	void Start () {
		isMoving = false;
		stateManager = Tools.loadStateManager();
		player = stateManager.getPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isMoving) {
			move ();
			if (timeCounter > 0) {
				timeCounter -= Time.deltaTime;
			} else {
				triggerStop ();
			}

		}
	}

	public void startMove(){
		Debug.Log ("START moving");
		isMoving = true;
		timeCounter = 1;
		pos = gameObject.transform.position;
	}

	void move(){
		//TODO but block down when state changing ?
		Debug.Log ("moving");
		pos = player.transform.position;
		Vector3 temp = new Vector3 (0f, 2f, 0f);
		gameObject.transform.position = pos + temp;
	}

	void stopMove(){
		Debug.Log ("STOP moving");
		isMoving = false;
		Vector3 temp = new Vector3 (1f, 0f, 0f);
		gameObject.transform.position = pos + temp;
	}

	void triggerStop(){
		if (Input.GetKeyDown (KeyCode.R)) {
			if (isMoving) {
				stopMove ();
			}
		}
	}
		
}
