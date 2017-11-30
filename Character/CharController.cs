using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharController : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	bool awakeScript;

	Vector3 forward, right;
	GameController gameController;

	AudioSource audio;
	[SerializeField]
	AudioClip snoringSound;
	[SerializeField]
	AudioClip footsteps;

	[SerializeField]
	float intervalBtwSnoring;
	float tempNum = 0;

	Animator animator;
	
	bool isMoving;
	int nbColl;

	public delegate void dying();
	public static event dying onFalling;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		setupIsometricVector ();
		isMoving = false;
		audio = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Tools.getState() == State.Asleep) && (awakeScript)) {
			animator.SetInteger("Direction", -2);
			snoring();
		} else {
			updateMovement();
		}
		if (isFallingDown()) {
			if (onFalling != null) {
				onFalling();
			}

		}
		
	}

	void snoring() {
		tempNum -= Time.deltaTime;
		if (tempNum <= 0){
			tempNum = intervalBtwSnoring;
			audio.PlayOneShot(snoringSound,0.9F);
		}
	}

	bool isFallingDown() {
		if (transform.position.y < -40f) {
			return true;
		} else {
			return false;
		}
	}

	void updateMovement() {
			float moveVertical = Input.GetAxis ("Vertical");
			float moveHorizontal = Input.GetAxis("Horizontal");
			isPlayerMoving(moveVertical, moveHorizontal);
			if (isMoving) {	
				if (Tools.getState() == State.Awake) {
					audio.pitch = gameController.tiredness();
					updateMovementIsometric (moveVertical, moveHorizontal, true);		
				} else {
					updateMovementIsometric (moveVertical, moveHorizontal, false);
				}
				assignAnimation(moveVertical, moveHorizontal);
			} else {
				animator.SetInteger("Direction", -1);
			}
	}


	void setupIsometricVector() {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize (forward);
		right = Quaternion.Euler (new Vector3 (0, 90, 0)) * forward;
	}


	void isPlayerMoving(float moveVertical, float moveHorizontal) {
		if ((moveVertical != 0) || (moveHorizontal != 0)) {
			if (!isMoving){
				isMoving = true;
				audio.Play();
			}
		} else {
			if (isMoving) {
				audio.Stop();
				isMoving = false;
			}
		}
	}

	void updateMovementIsometric(float moveVertical, float moveHorizontal, bool awake){
		float tempSpeed;
		if (awake) {
			tempSpeed = moveSpeed * gameController.tiredness();
		} else {
			tempSpeed = moveSpeed;
		}
		
		Vector3 rightMovement = right * tempSpeed * Time.deltaTime * moveHorizontal;
		Vector3 upMovememt = forward * tempSpeed * Time.deltaTime * moveVertical;
		Vector3 heading = Vector3.Normalize (rightMovement + upMovememt);
		gameController.setFwdIso(heading);
		transform.position += rightMovement;
		transform.position += upMovememt;
		
	}


	void assignAnimation (float moveVertical, float moveHorizontal){
		if ((moveVertical < 0) && (moveHorizontal == 0)) {
			/*FRONT*/
			animator.SetInteger("Direction", 0);
		} else if ((moveVertical > 0) && (moveHorizontal == 0)) {
			/*BACK*/
			animator.SetInteger("Direction", 4);
		} else if ((moveVertical == 0 ) && (moveHorizontal > 0)) {
			/*LEFT*/
			animator.SetInteger("Direction", 6);
		} else if ((moveVertical == 0 ) && (moveHorizontal < 0)) {
			/*RIGHT*/
			animator.SetInteger("Direction", 2);
		} else if ((moveVertical < 0 ) && (moveHorizontal < 0) ) {
			/*FRONTRIGHT*/
			animator.SetInteger("Direction", 1);
		} else if ((moveVertical < 0 ) && (moveHorizontal > 0) ) {
			/*FRONTLEFT*/
			animator.SetInteger("Direction", 7);
		} else if ((moveVertical > 0 ) && (moveHorizontal < 0) ) {
			/*BACKRIGHT*/
			animator.SetInteger("Direction", 3);
		} else if ((moveVertical > 0 ) && (moveHorizontal > 0) ) {
			/*BACKRLEFT*/
			animator.SetInteger("Direction", 5);
		}

	}

	public float getSpeed() {
		return moveSpeed;
	}

	// void OnCollisionEnter(){
	// 	nbColl ++;
	// }

	// void OnCollisionExit() {
	// 	nbColl --;
	// }

}
