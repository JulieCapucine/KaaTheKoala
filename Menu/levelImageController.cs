using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelImageController : MonoBehaviour {
	
	[SerializeField]
	Sprite level1;
	[SerializeField]
	Sprite level2;
	[SerializeField]
	Sprite level3;
	[SerializeField]
	Sprite lose;

	Image image;
	GameObject nextLvl;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		nextLvl = transform.GetChild(3).gameObject;

		if (Tools.win) {
			if(Tools.level == "Level1") {	
				image.sprite = level1;
			} else if (Tools.level == "Level2") {
				image.sprite = level2;
			} else if (Tools.level == "Level3") {
				image.sprite = level3;
				nextLvl.SetActive(false);
			}
		} else {
			image.sprite = level3;
			nextLvl.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
