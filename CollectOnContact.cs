using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnContact : MonoBehaviour {

	GameController gameController;

	public delegate void gainSeed();
	public static event gainSeed onGainingSeed;

	Collider colliderAsleep;

	void Start () {
		gameController = Tools.loadGameController();
		colliderAsleep = GetComponent<Collider>();
	}

	void update() {
		if (Tools.getState() == State.Asleep) {
			colliderAsleep.enabled = false;
		} else {
			colliderAsleep.enabled = true;
		}
	}

	void OnCollisionStay(Collision col) {
		if (Tools.getState() == State.Awake) {
			if (Input.GetKeyDown (KeyCode.E)) {
				if (col.gameObject.tag == "Trash") {
					gameController.addSeed (1);
					gameController.removeTrash (1);
					if (onGainingSeed != null) {
						onGainingSeed();
					}
					Destroy (col.gameObject);
				} else if (col.gameObject.tag == "Tree") {
					if (col.gameObject.GetComponent<TreeController> ().eatBranch ()) {
						gameController.gainEnergy (10);
					}
				}
			} 
		} else {

		}
	}


		
}
