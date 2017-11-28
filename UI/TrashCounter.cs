using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour {

	[SerializeField]
	Sprite trashGreen;
	[SerializeField]
	Sprite trashPurple;

	float nbTrash;
	int nbTrashMax;

	Text trashCounter;
	GameController gameController;

	public delegate void collecting();
	public static event collecting onAllCollected;

	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();
		nbTrashMax = GameObject.FindGameObjectsWithTag("Trash").Length;
		trashCounter = GetComponentInChildren<Text>();
		trashCounter.text = "0/" + nbTrashMax;
	}

	// Update is called once per frame
	void Update () {
		trashCounter.text = (nbTrashMax - gameController.getTrashCounter()) + "/" + nbTrashMax;
		if ((nbTrashMax - gameController.getTrashCounter()) == nbTrashMax) {
			GetComponentInChildren<Image>().sprite = trashGreen;
			trashCounter.color = new Color(0.3647f, 0.505f, 0.1176f);
			if (onAllCollected != null) {
				onAllCollected();
			}
		} 
	}
}
