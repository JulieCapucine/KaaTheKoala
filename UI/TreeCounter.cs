using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCounter : MonoBehaviour {

	public Image tree;
	private float nbTree;
	public int nbTreeMax;

	// Use this for initialization
	void Start () {
		tree.fillAmount = 0;
	}

	// Update is called once per frame
	void Update () {
		nbTree = GameObject.FindGameObjectsWithTag("Tree").Length;
		tree.fillAmount = nbTree / nbTreeMax;
	}
}
