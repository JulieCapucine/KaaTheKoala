using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	StateManager stateManager;
	Vector3 offset;
	GameObject player;

	Camera mainCam;

	[SerializeField]
	float cameraSpeed;

	Vector3 forward, right;
	 

	// Use this for initialization
	void Start () {
		mainCam = GetComponent<Camera>();
		player =  GameObject.FindGameObjectWithTag("Player");
		setupIsometricVector();
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		//offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		isPlayerNearEdge();
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		//transform.position = player.transform.position + offset;
		
	}

	void isPlayerNearEdge() {
		////Debug.Log(" player position :" + player.transform.position);
		Vector3 screenPoint = mainCam.WorldToViewportPoint(player.transform.position);
		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0.2 && screenPoint.x < 0.8 && screenPoint.y > 0.2 && screenPoint.y < 0.8;
		//Debug.Log(" player position in camera view :" + screenPoint + " bool : " + onScreen);
		//Debug.Log("bool IsOnScreen : " + onScreen);
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
		//Debug.Log(" x : " + moveHorizontal + " y :" + moveVertical);
		Vector3 rightMovement = right * cameraSpeed * Time.deltaTime * moveHorizontal;
		Vector3 upMovememt = forward * cameraSpeed * Time.deltaTime * moveVertical;
		Vector3 heading = Vector3.Normalize (rightMovement + upMovememt);
		//Debug.Log("right : " + rightMovement + "up : " + upMovememt);
		transform.position += rightMovement;
		transform.position += upMovememt;
		}
	}

	void setupIsometricVector() {
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize (forward);
		right = Quaternion.Euler (new Vector3 (0, 90, 0)) * forward;
	}
	
 
}
