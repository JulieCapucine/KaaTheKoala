using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

	[SerializeField]
	private Sprite night;
	[SerializeField]
	private Sprite day;


	void Start () {
	}

	void Update () {
		if (Tools.getState() == State.Awake) {
			gameObject.GetComponent<Image>().sprite = day;
		} else if (Tools.getState() == State.Asleep ) {
			gameObject.GetComponent<Image>().sprite = night;
		}
	}
}
