using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAttackController : NetworkBehaviour {

	public Animator playerAnimator;
	public PlayerArmController playerArmController;
	public PlayerState playerState;

	void Awake()
	{
		playerState = GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update() {
		if(Input.GetMouseButtonDown(0)){
			if(!playerState.isNetwork){
				playerAnimator.SetTrigger("Attack");
				playerAnimator.SetFloat("AttackCount", 0.0f);
			}
			else
			{
				playerArmController.Attack();
			}
		}
	}
}