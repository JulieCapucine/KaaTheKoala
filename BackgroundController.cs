using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

	[SerializeField]
	private Sprite night;
	[SerializeField]
	private Sprite day;

	StateManager stateManager;

	void Start () {
		stateManager = Tools.loadStateManager();
	}

	void Update () {
		if (stateManager.getState() == State.Awake) {
			gameObject.GetComponent<Image>().sprite = day;
		} else if (stateManager.getState() == State.Asleep ) {
			gameObject.GetComponent<Image>().sprite = night;
		}
	}
}
