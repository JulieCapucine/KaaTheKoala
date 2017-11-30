using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onMouseOver : MonoBehaviour {

	// Use this for initialization
	void Start(){
		GetComponent<Image>().enabled = false;
	}

	void OnMouseEnter(){
		GetComponent<Image>().enabled = true;
	}

	void OnMouseExit() {
		GetComponent<Image>().enabled = false;
	}
}
