using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : bl_PhotonHelper {
	public string playerName;
	public int playerHealth;

	void Start(){
		playerName = PhotonNetwork.player.NickName;
	}

	void Update(){
		PhotonNetwork.player.SetStatus (playerHealth);
	}
}