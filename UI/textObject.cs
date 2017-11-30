using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textObject : MonoBehaviour {

	[SerializeField]
	Sprite pressEnight;
	[SerializeField]
	Sprite pressEday;
	Image imgComponent;

	// Use this for initialization
	void Start () {
		imgComponent = GameObject.Find("InstructionText").GetComponent<Image>();
		imgComponent.enabled = false;

	}
	

	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.tag == "Player")) {
			imgComponent.enabled = true;
			if (Tools.getState() == State.Awake) {
				imgComponent.sprite = pressEday;
			}
			// } else if (Tools.getState() == State.Asleep) {
			// 	// if (this.tag == "Trash") {
			// 	// 	imgComponent.sprite = pressEnight;
			// 	// } else {
			// 	// 	imgComponent.enabled = false;
			// 	// }
				
			// }
			
			
		}
	}

	void OnTriggerExit(Collider other) {
		if ((other.gameObject.tag == "Player")) {
			imgComponent.enabled = false;
		}
	}

	void disableText () {
		imgComponent.enabled = false;
	}

	void OnEnable() {
		ObjectController.isDestroyed += disableText;
	}

	void OnDisable() {
		ObjectController.isDestroyed -= disableText;
	}

}
