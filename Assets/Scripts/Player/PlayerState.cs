using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*

 플레이어의 모든 상태를 관리한다.
 
 체력,
 스테미너,
 힘,
 스피드

 현재 상태

 */

public class PlayerState : NetworkBehaviour {
	public int health;
	public int stemina;
	public int power;
	public int speed;

	public bool isNetwork = false;
	
	public GameObject eraseBody;

	public PlayerStateType playerStateType;

	void Awake()
	{
		if(isNetwork || !isLocalPlayer)
			Destroy(eraseBody);
	}
}
