using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 offset;
	GameObject player;

	Camera mainCam;

	[SerializeField]
	float cameraSpeed;

	Vector3 forward, right;

	[SerializeField]
	bool moveEdges;
	[SerializeField]
	bool fixedCam;
	[SerializeField]
	bool followPlayer;
	 

	// Use this for initialization
	void Start () {
		mainCam = GetComponent<Camera>();
		player =  GameObject.FindGameObjectWithTag("Player");
		cameraSpeed = player.GetComponent<CharController>().getSpeed();
		if (moveEdges) {
			setupIsometricVector();
		} else if (followPlayer) {
			offset = transform.position - player.transform.position;
		}
		
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Tools.getState() != State.Done){
			if (moveEdges) {
				isPlayerNearEdge();
			} else if (followPlayer) {
				transform.position = player.transform.position + offset;
			}
		}
		
		
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		
		
	}

	void isPlayerNearEdge() {
		Vector3 screenPoint = mainCam.WorldToViewportPoint(player.transform.position);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0.2 && screenPoint.x < 0.8 && screenPoint.y > 0.2 && screenPoint.y < 0.8;
		if (!onScreen) {
			float moveHorizontal = 0, moveVertical = 0;
			if (screenPoint.x < 0.2) {
				moveHorizontal = -1;
			} else if (screenPoint.x > 0.8) {
				moveHorizontal = 1;
			} else if (screenPoint.y < 0.2) {
				moveVertical = -1;
			} else if (screenPoint.y > 0.8) {
				moveVertical = 1;
			}
		Vector3 rightMovement = right * cameraSpeed * Time.deltaTime * moveHorizontal;
		Vector3 upMovememt = forward * cameraSpeed * Time.deltaTime * moveVertical;
		transform.position += rightMovement;
		transform.position += upMovememt;
		}
	}

	void setupIsometricVector() {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize (forward);
		right = Quaternion.Euler (new Vector3 (0, 90, 0)) * forward;
		//transform.position = player.transform.position;
	}
	
 	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		 if (Tools.getState() == State.Asleep) {
		 	player =  GameObject.Find("GhostKoala(Clone)");
		 	offset = transform.position - player.transform.position;
		 	transform.position = player.transform.position + offset;
		 } else if (Tools.getState() == State.Awake) {
		 	player =  GameObject.Find("Koala");
		 }
		
	}
}
