using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	bool awakeScript;
	// Sprite frontKoala;
	// [SerializeField]
	// Sprite backKoala;
	// [SerializeField]
	// Sprite leftKoala;
	// [SerializeField]
	// Sprite rightKoala;
	// [SerializeField]
	// Sprite leftFrontKoala;
	// [SerializeField]
	// Sprite leftBackKoala;
	// [SerializeField]
	// Sprite rightFrontKoala;
	// [SerializeField]
	// Sprite rightBackKoala;

	Vector3 forward, right;
	GameController gameController;
	StateManager stateManager;
	AudioSource audio;
	Animator animator;
	
	bool isMoving;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		stateManager = Tools.loadStateManager();
		setupIsometricVector ();
		//setupSprite();
		isMoving = false;
		audio = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((stateManager.getState() == State.Asleep) && (awakeScript)) {
			animator.SetInteger("Direction", -2);
		} else {
			updateMovement();
		}
		
		
	}

	void updateMovement() {
			float moveVertical = Input.GetAxis ("Vertical");
			float moveHorizontal = Input.GetAxis("Horizontal");
			isPlayerMoving(moveVertical, moveHorizontal);
			if (isMoving) {	
				if (stateManager.getState() == State.Awake) {
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

	void setupSprite () {
		//frontKoala = GetComponent<SpriteRenderer> ().sprite;
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
			//GetComponent<SpriteRenderer> ().sprite = frontKoala;
			animator.SetInteger("Direction", 0);
		} else if ((moveVertical > 0) && (moveHorizontal == 0)) {
			//GetComponent<SpriteRenderer> ().sprite = backKoala;
			animator.SetInteger("Direction", 0);
		} else if ((moveVertical == 0 ) && (moveHorizontal > 0)) {
			//GetComponent<SpriteRenderer> ().sprite = rightKoala;
			animator.SetInteger("Direction", 6);
		} else if ((moveVertical == 0 ) && (moveHorizontal < 0)) {
			//GetComponent<SpriteRenderer> ().sprite = leftKoala;
			animator.SetInteger("Direction", 2);
		} else if ((moveVertical < 0 ) && (moveHorizontal < 0) ) {
			//GetComponent<SpriteRenderer> ().sprite = leftFrontKoala;
			animator.SetInteger("Direction", 1);
		} else if ((moveVertical < 0 ) && (moveHorizontal > 0) ) {
			//GetComponent<SpriteRenderer> ().sprite = rightFrontKoala;
			animator.SetInteger("Direction", 7);
		} else if ((moveVertical > 0 ) && (moveHorizontal < 0) ) {
			//GetComponent<SpriteRenderer> ().sprite = leftBackKoala;
			animator.SetInteger("Direction", 0);
		} else if ((moveVertical > 0 ) && (moveHorizontal > 0) ) {
			//GetComponent<SpriteRenderer> ().sprite = rightBackKoala;
			animator.SetInteger("Direction", 0);
		}

	}

}
