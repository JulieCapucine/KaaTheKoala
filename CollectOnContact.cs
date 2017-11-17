using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOnContact : MonoBehaviour {

	private GameController gameController;
	private StateManager stateManager;

	public delegate void gainSeed();
	public static event gainSeed onGainingSeed;

	void Start () {
		gameController = Tools.loadGameController();
		stateManager = Tools.loadStateManager();
	}

	void OnCollisionStay(Collision col) {
		if (stateManager.getState() == State.Awake) {
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
		}
	}


		
}
