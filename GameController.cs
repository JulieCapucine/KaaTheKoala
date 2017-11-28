using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//UI Feedback
	// public GUIText trashText;
	// public GUIText seedText;
	// public GUIText stepText;

	//Counter displayed in the UI
	int trashCounter, seedCounter;
	float tempTime = 0, timeBeforeLosing;

	[SerializeField]
	int treeObjective;


	//Manager between the Awake Koala Player and the GhostKoala player
	StateManager stateManager;
	GameObject player;

	AudioSource audio;

	//Tree object
	[SerializeField]
	Transform tree;

	Vector3 forward;

	[SerializeField]
	float tirednessThreshold;

	public delegate void planting();
	public static event planting onPlanting;

	bool allTrashCollected, allTreePlanted, hasNoLifeLeft = false, hasWon = false, hasLost = false;
	
	

	// Use this for initialization
	void Start () {
		stateManager = Tools.loadStateManager();
		player =  GameObject.FindGameObjectWithTag("Player");
		seedCounter = 0;
		trashCounter = GameObject.FindGameObjectsWithTag("Trash").Length;
		audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		//textUpdate();
		if (hasWon) {
			endGameMessage("YOU WIN !");
		} else if (hasLost) {
			endGameMessage("YOU LOSE !");
		} else {
			updateDistance();
			if (stateManager.getState() == State.Awake) {
				keyboardInputAwake ();
			}
		}
	}

	public void setFwdIso(Vector3 fwd){
		forward = fwd;
	}

	//AWAKE KOALA
	public void addSeed(int num) {
		seedCounter += num;
	}

	void noLife() {
		hasLost = true;
	}

	void endGameMessage(string str) {
		tempTime += Time.deltaTime;
			if (tempTime >= timeBeforeLosing) {
				GameObject textEndObject = GameObject.FindGameObjectWithTag("Finish");
				if (textEndObject != null) {
					Text textEnd = textEndObject.GetComponent<Text>();
					textEnd.enabled = true;
					textEnd.text = str;
				}
				if (GameObject.Find("UICanvas") != null ) {
					GameObject.Find("UICanvas").SetActive(false);
				}
				
				player.SetActive(false);
				stateManager.setState(State.Done);
				player.GetComponent<Rigidbody>().isKinematic = true;
			}
	}

	void keyboardInputAwake (){
		if(Input.GetKeyDown(KeyCode.R)) {
			plantSeed ();
			if (onPlanting != null) {
						onPlanting();
			}
		}
	}
		
	void plantSeed() {
		if (seedCounter > 0) {
			/*Calculate coordianate of the tree*/
			player =  GameObject.FindGameObjectWithTag("Player");
			Vector3 pos = forward*5 + player.transform.position;
			pos.y = tree.position.y;

			/*Checks tree is on the Ground / on the map */
			RaycastHit hit;
			Vector3 fwd = new Vector3 (0,-1,0);
			bool hitSomething = Physics.Raycast (pos, fwd, out hit);
			if (hitSomething && (hit.collider.gameObject.tag == "Ground")) {
				Instantiate(tree, pos, Quaternion.identity);
				seedCounter--;
			}
		}

	}

	public void removeTrash(int num) {
		trashCounter -= num;
		audio.Play();
	}

	/* Calculate a tiredness coefficient relating to the distance travelled by the Koala */
	public float tiredness(){
		float tirednessCoef = (stateManager.getDistanceMax() - stateManager.getDistanceTravelled()) / stateManager.getDistanceMax();
		if (tirednessCoef <= tirednessThreshold) { tirednessCoef = tirednessThreshold; }
		return tirednessCoef;
	}

	public int getTreeObjective() {
		return treeObjective;
	}

		
	void updateDistance() {
		if (stateManager.getState() == State.Awake) {
			stateManager.addDistance(Vector3.Distance(player.transform.position, stateManager.getLastPosition()));
			stateManager.setLastPosition(player.transform.position);
		}
	}

	public void gainEnergy(int num) {
		stateManager.addDistance(- num);
	}
		
	//UI_UPDATE 
	// void textUpdate() {
	// 	trashText.text = "trash left: " + trashCounter;
	// 	seedText.text = "seeds in Kaa pooch: " + seedCounter;
	// 	if (stateManager.getState() == State.Awake) {
	// 		stepText.text = "distance left before falling asleep:" + Mathf.Floor (stateManager.getDistanceMax() - stateManager.getDistanceTravelled());
	// 	} else {
	// 		stepText.text = "time remaining : " + Mathf.Floor (stateManager.getTime().x);
	// 	}
	// }

	public int getSeedCounter() {
		return seedCounter;
	}

	public int getTrashCounter() {
		return trashCounter;
	}

	void allTrash(){
		allTrashCollected = true;
		if (allTreePlanted) {
			hasWon = true;
			timeBeforeLosing = 1;
		}
	}

	void allTree(){
		allTreePlanted = true;
		if (allTrashCollected == true) {
			hasWon = true;
			timeBeforeLosing = 1;
		}
	}

	void fallingOutOfMap() {
		hasLost = true;
		timeBeforeLosing = 5;
	}


	void OnEnable() {
		TrashCounter.onAllCollected += allTrash;
		TreeCounter.onAllPlanted	+= allTree;
		StateManager.noLifeLeft += noLife;
		CharController.onFalling += fallingOutOfMap;
	}

	void OnDisable() {
		TrashCounter.onAllCollected -= allTrash;
		TreeCounter.onAllPlanted	-= allTree;
		StateManager.noLifeLeft -= noLife;
		CharController.onFalling -= fallingOutOfMap;
	}

}
