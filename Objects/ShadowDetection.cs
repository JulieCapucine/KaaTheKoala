using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDetection : MonoBehaviour {


	public delegate void shadowIn();
	public static event shadowIn inShadow;

	public delegate void shadowOut();
	public static event shadowOut outShadow;

	[SerializeField]
	float speed;	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Tools.getState() == State.Asleep) {
			rotativeShadow();
		}
	}

	void rotativeShadow() {
		transform.Rotate(Vector3.forward * speed);
	}

	void OnTriggerEnter() {
		if (inShadow != null) {
			inShadow();
		}
	}

	void OnTriggerExit() {
		if (outShadow != null) {
			outShadow();
		}
	}

	
}
