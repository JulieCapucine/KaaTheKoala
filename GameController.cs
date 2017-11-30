using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


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
		setLevel();
	}
	
	// Update is called once per frame
	void Update () {
		//textUpdate();
		if (hasWon) {
			Tools.win = true;
			SceneManager.LoadScene("PlayAgain", LoadSceneMode.Single);
		} else if (hasLost) {
			Tools.win = false;
			SceneManager.LoadScene("PlayAgain", LoadSceneMode.Single);
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

	void setLevel() {
		Scene scene = SceneManager.GetActiveScene();
		string name = scene.name;
		Tools.level = name;
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
		timeBeforeLosing = 1;
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
