using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

	Shader shaderDay;
	Shader shaderNight;

	// Use this for initialization
	void Start () {
		shaderDay = Shader.Find("Unlit/Color");
		shaderNight = Shader.Find("Nature/Terrain/Diffuse");
	}	
	
	// Update is called once per frame
	void Update () {
		
	}

	void setNewShader(Shader shader) {
		foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
     		if(gameObj.name == "Top") {
         		gameObj.GetComponent<MeshRenderer>().material.shader = shader;
    		}
 		}
	}

	void OnEnable() {
		StateManager.changeStateHppnd += changeStateHppnd;
	}

	void OnDisable() {
		StateManager.changeStateHppnd -= changeStateHppnd;
	}

	void changeStateHppnd() {
		if (Tools.getState() == State.Awake) {
			setNewShader(shaderDay);
		 } else {
		 	setNewShader(shaderNight);
		 }
		
	}
}
