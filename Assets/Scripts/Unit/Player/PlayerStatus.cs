using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	public string playerName;
	public int playerHealth;

	void Start(){
		playerName = PhotonNetwork.player.NickName;
	}
}