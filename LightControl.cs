using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour {

	public Light light;
	GameObject spotLight_game;
	public Transform spotLight;
	StateManager stateManager;
	bool isSpotLightInstentiated;
	GameObject player;

	int temp = 0;
	int direction = 1;
	

	void Start () {
		stateManager = Tools.loadStateManager();
		player = stateManager.getPlayer();
		light.intensity = 2;
		isSpotLightInstentiated = false;
	}
	
	
	void Update () {
		if (stateManager.getState() == State.Awake) {
			setAmbienceLight (2f);
			if (isSpotLightInstentiated) {
				destroySpotlight ();
			}
		} else if (stateManager.getState() == State.Asleep) {
			setAmbienceLight(0.5f);
			Vector3 position = new Vector3(395.3f, 97f, 0f);
			Vector2 angleRange = new Vector2 (266, 215);
			if ((!spotLight.gameObject.activeInHierarchy) && (!isSpotLightInstentiated)){
				createSpotLight (position);
			}
			//randomSpotLight (0.1f);

			movingLateralSpotLight(0.4f, angleRange); 
			if (isPlayerInLight ()) {
				stateManager.looseHealth ();
			}

		}

	}

	void randomSpotLight(float speed) {
		float moveHorizontal = Random.Range (-1, 1);
		float moveVertical = Random.Range (-1, 1);
		Vector3 pos = new Vector3 (spotLight_game.transform.position.x + moveHorizontal * speed, spotLight_game.transform.position.y, spotLight_game.transform.position.z + moveVertical * speed);
		spotLight_game.transform.position = pos;
		Debug.Log ("move" + spotLight.position);
		//Debug.Log ("pos" + pos);

	}

	void movingLateralSpotLight(float speed, Vector2 angleRange) {
		if (spotLight_game.transform.eulerAngles.y >=  angleRange.x) {
			direction = -1;
			Debug.Log ("here");
		} else if (spotLight_game.transform.eulerAngles.y <= angleRange.y) {
			direction = 1;
			Debug.Log ("hereAgain");
		}
		Quaternion rotation = Quaternion.Euler (spotLight_game.transform.eulerAngles.x, spotLight_game.transform.eulerAngles.y + speed * direction, spotLight_game.transform.eulerAngles.z);
		spotLight_game.transform.rotation = rotation;
		//Debug.Log ("rotation" + spotLight_game.transform.eulerAngles.y);

	}


	void destroySpotlight(){
		if (isSpotLightInstentiated) {
			Destroy (spotLight_game.gameObject);
			isSpotLightInstentiated = false;
		}
	}

	void createSpotLight(Vector3 position){
		spotLight_game = Instantiate(spotLight, player.transform.position + position, Quaternion.Euler (23.3f, -91f, -90f)).gameObject;
		isSpotLightInstentiated = true;
		Debug.Log (spotLight_game.transform.position);
	}

	void setAmbienceLight(float intensity){
		light.intensity = intensity;
	}

	bool isPlayerInLight (){
		RaycastHit hit;
		Vector3 fwd = spotLight_game.transform.TransformDirection(Vector3.forward);
		bool touchByLight = Physics.Raycast (spotLight_game.transform.position, fwd, out hit);
		if (touchByLight && (hit.collider.gameObject.name == "Terrain")) {
			touchByLight = isPlayerRadius(hit.point);
		}
//		if (touchByLight) {
//			touchByLight = !isPlayerInShadow ();
//		}

		return touchByLight;
	}

	bool isPlayerRadius(Vector3 point){
		Light lt = spotLight_game.GetComponent<Light> ();
		double radius = lt.range*0.5;
		if (player.transform.position.x > point.x - radius) {
			if (player.transform.position.x < point.x + radius) {
				if (player.transform.position.z > point.z - radius) {
					if (player.transform.position.z < point.z + radius) {
						temp++;
						if (isPlayerInShadow ()) {
							return false;
						} else {
							//Debug.Log ("DEAD" + temp);
							return true;
						}

					}
				}
			}
		}
		return false;
	}

	bool isPlayerInShadow() {
//		Vector3 fwd = player.transform.TransformDirection(Vector3.forward);
//		bool inShadow = Physics.Raycast (player.transform.position, fwd, 50);
//		if (inShadow) {
//			Debug.Log ("In the shadow dude!");
//		}
//		return inShadow;

		RaycastHit hit;
		Vector3 fwd = player.transform.TransformDirection(-spotLight_game.transform.forward);
		bool inShadow = Physics.Raycast (player.transform.position, fwd, out hit);
		if (inShadow) {
			Debug.Log ("In the shadow dude!" + hit.collider.tag);
		}
		return inShadow;
	}
}
