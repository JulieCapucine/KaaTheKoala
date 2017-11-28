using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textObject : MonoBehaviour {

	[SerializeField]
	Sprite pressE;
	Image imgComponent;

	// Use this for initialization
	void Start () {
		imgComponent = GameObject.Find("InstructionText").GetComponent<Image>();
		imgComponent.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.tag == "Trash") || (other.gameObject.tag == "Tree")) {
			imgComponent.enabled = true;
			imgComponent.sprite = pressE;
		}
	}

	void OnTriggerExit(Collider other) {
		imgComponent.enabled = false;
	}
}
