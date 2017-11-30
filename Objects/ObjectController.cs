using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController: MonoBehaviour {

	Vector3 pos;
	float timeCounter;

	GameObject player;
	StateManager stateManager;
	GameController gameController;

	Rigidbody rb;

	[SerializeField]
	Sprite pressE;
	Image imgComponent;

	public delegate void destroyTrash();
	public static event destroyTrash isDestroyed;

	public delegate void gainSeed();
	public static event gainSeed onGainingSeed;

	bool destroy = false;

	int collision = 0;
	
	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		player = stateManager.getPlayer();
		rb = GetComponent<Rigidbody>();
		rb.isKinematic = true;
		GetComponents<Collider>()[0].enabled = true;
		imgComponent = GameObject.Find("InstructionText").GetComponent<Image>();
		imgComponent.enabled = false;
		gameController = Tools.loadGameController();
	}
	
	// Update is called once per frame
	void Update () {
		if (destroy) {
			imgComponent.enabled = false;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (Tools.getState() == State.Asleep) {
			if (col.gameObject.tag == "Player") {
				rb.isKinematic = false;
			}
		}
		collision ++;
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Player") {

			if (Tools.getState() == State.Awake) {

				if (Input.GetKeyUp (KeyCode.E)) {

						if (!destroy) {
							
							gameController.addSeed (1);
							gameController.removeTrash (1);
							if (onGainingSeed != null) {
								onGainingSeed();
							}
							if (isDestroyed != null) {
								isDestroyed();
							}
							Destroy (gameObject);
							destroy = true;
						}
				} 

			 } else if (Tools.getState() == State.Asleep) {
			 	if (Input.GetKeyDown (KeyCode.E)) {
					//rb.isKinematic = false;
					//GetComponents<Collider>()[0].enabled = true;
				} else if (Input.GetKeyUp(KeyCode.E)) {
					//rb.isKinematic = true;
					//GetComponents<Collider>()[0].enabled = false;
				}

			 }
		}
	}

	void OnTriggerExit (Collider col) {
		if (Tools.getState() == State.Asleep) {
			if (col.gameObject.tag == "Player") {
				rb.isKinematic = true;
			}
		}
		collision--;
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;

	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		 imgComponent.enabled = false;
		
	}
		
}
