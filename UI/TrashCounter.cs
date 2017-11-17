using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCounter : MonoBehaviour {

	public Image trash;
	private GameController gameController;
	private float nbTrash;


	// Use this for initialization
	void Start () {
		gameController = Tools.loadGameController();

		nbTrash = GameObject.FindGameObjectsWithTag("Trash").Length;
		trash.fillAmount = 1;
	}

	// Update is called once per frame
	void Update () {
		trash.fillAmount = gameController.getTrashCounter() / nbTrash;
	}
}
