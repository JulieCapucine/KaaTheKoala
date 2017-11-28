using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State { Awake, Asleep, Done };

public class StateManager : MonoBehaviour {

	State state;

	GameObject playerAwake;
	GameObject playerAsleep;
	GameObject player;

	public delegate void changeState();
	public static event changeState changeStateHppnd;

	public delegate void life();
	public static event life noLifeLeft;

	[SerializeField]
	Transform ghostKoala;

	//Distance to travel before being too tired and falling asleep
	[SerializeField]
	int distanceMax;
	float currentDistanceTravelled;
	Vector3 lastPosition; //usefull to calculate the distance

	//Timer during Asleep State
	[SerializeField]
	float timeMax;
	float currentTime;

	//Health
	[SerializeField]
	float healthMax;
	[SerializeField]
	float currentHealth;
	float tempTime = 1;

	[SerializeField]
	AudioClip dayAmbiance;
	[SerializeField]
	AudioClip nightAmbiance;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		startAwake ();
	}
	
	// Update is called once per frame
	void Update () {
		timerUpdate();
		changeStateControle();
		
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene("PlayAgainMenu", LoadSceneMode.Single);
		}
	}

	void startAwake() {
		playerAwake = GameObject.Find("Koala");
		player = playerAwake;
		state = State.Awake;
		currentDistanceTravelled = 0;
		lastPosition = player.transform.position;
		currentHealth = healthMax;
		audio.clip = dayAmbiance;
		audio.Play();
	}

	void changeStateControle() {
		if (state == State.Awake) {
			if (currentDistanceTravelled >= distanceMax) {
				
				//playerAwake.GetComponent<KoalaController>().Sleeping();
				playerAwake.GetComponent<Rigidbody>().isKinematic = true;
				Vector3 position = playerAwake.transform.position + playerAwake.transform.forward;
				position.y = ghostKoala.position.y;
				playerAsleep = Instantiate (ghostKoala, position, ghostKoala.transform.rotation).gameObject;
				player = playerAsleep;
				currentTime = timeMax;
				state = State.Asleep;
				audio.clip = nightAmbiance;
				audio.Play();
				//AudioClip.PlayOneShot(timerSound, 0.7F);
				// timerSound.Play();
				if (changeStateHppnd != null) {
						changeStateHppnd();
				}
			}
		} else if (state == State.Asleep) {
			if (currentTime <= 0) {
				
				//playerAwake.GetComponent<KoalaController>().Awake();
				playerAwake.GetComponent<Rigidbody>().isKinematic = false;
				Destroy (playerAsleep);
				state = State.Awake;
				currentDistanceTravelled = 0;
				lastPosition = player.transform.position;
				// timerSound.Stop();
				audio.clip = dayAmbiance;
				audio.Play();
				if (changeStateHppnd != null) {
						changeStateHppnd();
				}
				player = playerAwake;
			}
		} else if (state == State.Done) {
			player.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void timerUpdate() {
		if (state == State.Asleep) {
			currentTime -= Time.deltaTime;

		}
	}

	public Vector2 getTime() {
		Vector2 time = new Vector2(currentTime, timeMax);
		return time;
	}

	public Vector2 getHealth() {
		Vector2 health = new Vector2(currentHealth, healthMax);
		return health;
	}

	public void looseHealth(int num){
		currentHealth -= num;
		if (currentHealth == 0) {
			if (noLifeLeft != null) {
				noLifeLeft();
			}
		} 
	}

	public GameObject getPlayer() {
		return player;
	}

	public State getState() {
		return state;
	}

	public float getDistanceTravelled(){
		return currentDistanceTravelled;
	}

	public void addDistance(float dist) {
		currentDistanceTravelled += dist;
	}

	public void setLastPosition(Vector3 pos) {
		lastPosition = pos;
	}

	public Vector3 getLastPosition() {
		return lastPosition;
	}

	public int getDistanceMax() {
		return distanceMax;
	}

	public void setState(State newState) {
		state = newState;
	}


}
