using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {


	private int nbBranch;
	public int nbBranchMax;
	//private TextMesh textMesh;

	public Sprite twoBranch;
	public Sprite oneBranch;
	public Sprite empty;


	AudioSource audio;
	[SerializeField]
	AudioClip eatLeaves;

	Animator animator;
	GameController gameController;

	[SerializeField]
	int energyFromLeaf;

	float currentTime;
	float maxTime = 1;

	// Use this for initialization
	void Start () {
		setUpBranch ();
		audio = GetComponent<AudioSource>();
		GetComponent<Collider>().enabled = true;
		animator = GetComponentInChildren<Animator>();
		gameController = Tools.loadGameController();
	}	

	void Update() {
		if ((currentTime > 0 ) && (currentTime <= maxTime)){
			currentTime += Time.deltaTime;
		} else {
			currentTime = 0;
		}
	}

	void setUpBranch (){
		nbBranch = nbBranchMax;
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.tag == "Player") {
			if (Tools.getState() == State.Awake) {
				if (Input.GetKeyDown (KeyCode.E)) {
					if (currentTime == 0) {
						if (eatBranch ()) {
							gameController.gainEnergy (energyFromLeaf);
						}
						currentTime += Time.deltaTime;
					}
					
				}
			}
		}
	}

	public bool eatBranch(){
		audio.clip = eatLeaves;
		if (nbBranch > 0) {
			nbBranch--;
			audio.Play();
			switch (nbBranch) {
				case 2:
					animator.SetInteger("idleAnimation", 2);
					break;
				case 1:
					animator.SetInteger("idleAnimation", 1);
					break;
				case 0:
					animator.SetInteger("idleAnimation", 0);
					break;
				default :
					break;
					
			}
			Debug.Log(animator.GetInteger("idleAnimation"));
			return true;
		} 
		Debug.Log(animator.GetInteger("idleAnimation"));
		return false;
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		 if (Tools.getState() == State.Asleep) {
		 	GetComponent<Collider>().enabled = false;
		 } else if (Tools.getState() == State.Awake) {
		 	GetComponent<Collider>().enabled = true;
		 }
		
	}

}
	