using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightManager : MonoBehaviour {

	GameObject child;
	// Use this for initialization
	void Start () {
		child = GameObject.Find("GroundLight").gameObject;
		if (Tools.getState() == State.Awake){
			child.SetActive(false);
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
		if (Tools.getState() == State.Asleep) {
			child.SetActive(true);
		 } else {
		 	child.SetActive(false);
		 }
		
	}
}
