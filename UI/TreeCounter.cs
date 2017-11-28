using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCounter : MonoBehaviour {

	[SerializeField]
	Sprite treeGreen;
	[SerializeField]
	Sprite treePurple;

	private float nbTree;
	int nbTreeMax;

	Text treeCounter;

	public delegate void planting();
	public static event planting onAllPlanted;

	// Use this for initialization
	void Start () {
		nbTreeMax = Tools.loadGameController().getTreeObjective();
		treeCounter = GetComponentInChildren<Text>();
		treeCounter.text = "0/" + nbTreeMax;
	}

	// Update is called once per frame
	void Update () {
		nbTree = GameObject.FindGameObjectsWithTag("Tree").Length;
		treeCounter.text = nbTree + "/" + nbTreeMax;
		if (nbTree == nbTreeMax) {
			GetComponentInChildren<Image>().sprite = treeGreen;
			treeCounter.color = new Color(0.3647f, 0.505f, 0.1176f);
			if (onAllPlanted != null) {
				onAllPlanted();
			}
		} 
	}
}
