using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaController : MonoBehaviour {

	public Sprite AsleepSprite;
	public Sprite AwakeSprite;

	public void Sleeping() {
		GetComponent<SpriteRenderer> ().sprite = AsleepSprite;
	}
		
	public void Awake() {
		GetComponent<SpriteRenderer> ().sprite = AwakeSprite;
	}
}
	