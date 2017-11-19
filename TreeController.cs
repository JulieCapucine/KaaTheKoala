using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {


	private int nbBranch;
	public int nbBranchMax;
	//private TextMesh textMesh;

	public Sprite twoBranch;
	public Sprite oneBranch;
	public Sprite empty;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		setUpBranch ();
		audio = GetComponent<AudioSource>();
	}	

	void setUpBranch (){
		nbBranch = nbBranchMax;
		//textMesh = gameObject.GetComponentInChildren<TextMesh>();
		//textMesh.text = nbBranch + " / " + nbBranchMax;
		Debug.Log (nbBranch + " / " + nbBranchMax);
	}

	public bool eatBranch(){
		if (nbBranch > 0) {
			nbBranch--;
			audio.Play();
			Debug.Log (nbBranch + " / " + nbBranchMax);
			switch (nbBranch) {
				case 2:
					GetComponentInChildren<SpriteRenderer>().sprite = twoBranch;
					break;
				case 1:
					GetComponentInChildren<SpriteRenderer>().sprite = oneBranch;
					break;
				case 0:
					GetComponentInChildren<SpriteRenderer>().sprite = empty;
					break;
				default :
					break;
					
			}

			return true;
		} 
		return false;
	}

}
	