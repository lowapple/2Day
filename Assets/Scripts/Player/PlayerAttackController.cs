using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

	public Animator playerAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			playerAnimator.SetTrigger("Attack");
			playerAnimator.SetFloat("AttackCount", 0.0f);
		}
		if(Input.GetMouseButtonDown(1)){
			Debug.Log("Right");
		}		
	}

	void FixedUpdate()
	{

	}
}
