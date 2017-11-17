using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");

		if (horizontal > 0)
		{
			
			animator.SetInteger("Direction", 10);
		}
		else if (horizontal < 0)
		{
			animator.SetInteger("Direction", -1);
		}
	}
}
