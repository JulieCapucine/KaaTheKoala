using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLight : MonoBehaviour {

	[SerializeField]
	float speed;

	[SerializeField]
	float radiusMvt;

	[SerializeField]
	int nbLifePoints;

	[SerializeField]
	Vector3 centerOrbit;

	Vector3 direction;
	Vector3 front;
	Vector3 back;
	Vector3 left;
	Vector3 right;

	Vector3 pos;

	StateManager stateManager;

	[SerializeField]
	float timeInLightHurtfull;
	float tempNum = 0;

	bool isProtected = false;

	bool temp;

	GameObject parent;

	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		parent = GameObject.FindWithTag("EnemyLight").gameObject;
		setupCircularMovement();
		
	}
	
	// Update is called once per frame
	void Update () {
		temp = isOnGround();
		circularMovement();
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.name == "GhostKoala(Clone)") {
			if (!isProtected) {
					tempNum += Time.deltaTime;
				if (tempNum >= timeInLightHurtfull) {
					tempNum = 0;
					stateManager.looseHealth(nbLifePoints);
				} 
			}
			
		}
	}


	void setupTransversalMovement() {
		front = new Vector3 (0,0,-1);
		back = new Vector3 (0,0,1);
		left = new Vector3 (-1,0,0);
		right = new Vector3 (1,0,0);
		direction = right;
	}

	bool isOnGround() {
		float radius = 10f;
		pos = transform.position;
		Vector3 tempPos = pos + front * radius; 
		if (Tools.isPointOnGround(tempPos)) {
			tempPos = pos + back * radius; 
			if (Tools.isPointOnGround(tempPos)) {
				tempPos = pos + left * radius; 
				if (Tools.isPointOnGround(tempPos)) {
					tempPos = pos + right * radius; 
					if (Tools.isPointOnGround(tempPos)) {
						return true;
					}
				}
			}
		}
		return false;
	}

	void transversalMovement(){
		pos = gameObject.transform.position + direction * speed;
		if (!Tools.isPointOnGround(pos)) {
			if (direction.Equals(right)) {
				direction = front;
			} else if (direction.Equals(front)) {
				direction = left;
			} else if (direction.Equals(left)) {
				direction = front;
			} else if (direction.Equals(back)) {
				direction = right;
			} 
		}
		pos.y = 30;
		pos = gameObject.transform.position + direction * speed;
		gameObject.transform.position = pos;

	}

	void setupCircularMovement() {
		// pos = new Vector3 (radiusMvt, parent.transform.position.y, 0);
		// parent.transform.position = pos;
	}

	void circularMovement() {
		transform.RotateAround(centerOrbit, Vector3.up, speed);
	}

	void OnEnable() {
		ShadowDetection.inShadow += isInShadow;
		ShadowDetection.outShadow += isOutShadow;
	}

	void OnDisable() {
		ShadowDetection.inShadow -= isInShadow;
		ShadowDetection.outShadow -= isOutShadow;
	}

	void isInShadow (){
		isProtected = true;
	}

	void isOutShadow (){
		isProtected = false;
	}
}
