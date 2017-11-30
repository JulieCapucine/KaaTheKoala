using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightManager : MonoBehaviour {

	GameObject[] child;
	// Use this for initialization
	void Start () {
		child = GameObject.FindGameObjectsWithTag("GroundLight");
		if (Tools.getState() == State.Awake){
			foreach(GameObject gameObj in child) {
				gameObj.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		if (Tools.getState() == State.Asleep){
			foreach(GameObject gameObj in child) {
				gameObj.SetActive(true);
			}
		} else {
		 	foreach(GameObject gameObj in child) {
				gameObj.SetActive(false);
			}
		 }
		
	}
}
