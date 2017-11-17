using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	Sprite frontKoala;
	[SerializeField]
	Sprite backKoala;
	[SerializeField]
	Sprite leftKoala;
	[SerializeField]
	Sprite rightKoala;
	[SerializeField]
	Sprite leftFrontKoala;
	[SerializeField]
	Sprite leftBackKoala;
	[SerializeField]
	Sprite rightFrontKoala;
	[SerializeField]
	Sprite rightBackKoala;

	Vector3 forward, right;
	GameController gameController;
	AudioSource audio;
	//Animator animator;
	
	bool isMoving;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		setupIsometricVector ();
		setupSprite();
		isMoving = false;
		audio = GetComponent<AudioSource>();
		//animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");
		isPlayerMoving(moveVertical, moveHorizontal);

		if (isMoving) {	
			updateMovementIsometric (moveVertical, moveHorizontal);
			assignSprite(moveVertical, moveHorizontal);
		}
		
	}

	void setupIsometricVector() {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize (forward);
		right = Quaternion.Euler (new Vector3 (0, 90, 0)) * forward;
	}

	void setupSprite () {
		frontKoala = GetComponent<SpriteRenderer> ().sprite;
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

	void updateMovementIsometric(float moveVertical, float moveHorizontal){
		float tempSpeed = moveSpeed * gameController.tiredness();
		Vector3 rightMovement = right * tempSpeed * Time.deltaTime * moveHorizontal;
		Vector3 upMovememt = forward * tempSpeed * Time.deltaTime * moveVertical;
		Vector3 heading = Vector3.Normalize (rightMovement + upMovememt);
		transform.position += rightMovement;
		transform.position += upMovememt;
		
	}


	void assignSprite (float moveVertical, float moveHorizontal){
		if ((moveVertical < 0) && (moveHorizontal == 0)) {
			GetComponent<SpriteRenderer> ().sprite = frontKoala;
		} else if ((moveVertical > 0) && (moveHorizontal == 0)) {
			GetComponent<SpriteRenderer> ().sprite = backKoala;
		} else if ((moveVertical == 0 ) && (moveHorizontal > 0)) {
			GetComponent<SpriteRenderer> ().sprite = rightKoala;
			//animator.SetInteger("AnimationNb", 3);
		} else if ((moveVertical == 0 ) && (moveHorizontal < 0)) {
			GetComponent<SpriteRenderer> ().sprite = leftKoala;
			//animator.SetInteger("AnimationNb", 7);
		} else if ((moveVertical < 0 ) && (moveHorizontal < 0) ) {
			GetComponent<SpriteRenderer> ().sprite = leftFrontKoala;
		} else if ((moveVertical < 0 ) && (moveHorizontal > 0) ) {
			GetComponent<SpriteRenderer> ().sprite = rightFrontKoala;
		} else if ((moveVertical > 0 ) && (moveHorizontal < 0) ) {
			GetComponent<SpriteRenderer> ().sprite = leftBackKoala;
		} else if ((moveVertical > 0 ) && (moveHorizontal > 0) ) {
			GetComponent<SpriteRenderer> ().sprite = rightBackKoala;
		}

	}

}
