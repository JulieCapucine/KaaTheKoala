using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOver : MonoBehaviour {

	// Use this for initialization
	void Start(){
		GetComponent<Renderer>().material.color = Color.black;
	}

	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.grey;
	}

	void OnMouseExit() {
		GetComponent<Renderer>().material.color = Color.black;
	}
}
