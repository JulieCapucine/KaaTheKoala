using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIstateManager : MonoBehaviour {

	private StateManager stateManager;
	public bool isAwake;
	public bool isAsleep;
	private Image [] allRenderers;

	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((stateManager.getState() == State.Awake) && (isAwake)) {
			gameObject.GetComponent<Image>().enabled = true;
			allRenderers = gameObject.GetComponentsInChildren< Image >();
			foreach ( Image childRenderer in allRenderers )
			{
				childRenderer.enabled = true;
			}
//			
		} else if ((stateManager.getState() == State.Asleep) && (isAwake)){
			gameObject.GetComponent<Image>().enabled = false;
			allRenderers = gameObject.GetComponentsInChildren< Image >();
			foreach ( Image childRenderer in allRenderers )
			{
				childRenderer.enabled = false;
			}
			//			

		} if ((stateManager.getState() == State.Awake) && (isAsleep)) {
			gameObject.GetComponent<Image>().enabled = false;
			allRenderers = gameObject.GetComponentsInChildren< Image >();
			foreach ( Image childRenderer in allRenderers )
			{
				childRenderer.enabled = false;
			}

		} else if ((stateManager.getState() == State.Asleep) && (isAsleep)){
			gameObject.GetComponent<Image>().enabled = true;
			allRenderers = gameObject.GetComponentsInChildren< Image >();
			foreach ( Image childRenderer in allRenderers )
			{
				childRenderer.enabled = true;
			}

		}
	}

}
