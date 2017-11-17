using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {


	private int nbBranch;
	public int nbBranchMax;
	private TextMesh textMesh;

	public Sprite twoBranch;
	public Sprite oneBranch;
	public Sprite empty;

	// Use this for initialization
	void Start () {
		setUpBranch ();
	}	

	void setUpBranch (){
		nbBranch = nbBranchMax;
		textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.text = nbBranch + " / " + nbBranchMax;
		Debug.Log (textMesh.text);
	}

	public bool eatBranch(){
		if (nbBranch > 0) {
			nbBranch--;
			textMesh.text = nbBranch + " / " + nbBranchMax;
			switch (nbBranch) {
				case 2:
					GetComponent<SpriteRenderer>().sprite = twoBranch;
					break;
				case 1:
					GetComponent<SpriteRenderer>().sprite = oneBranch;
					break;
				case 0:
					GetComponent<SpriteRenderer>().sprite = empty;
					break;
				default :
					break;
					
			}

			return true;
		} 
		return false;
	}

}
	