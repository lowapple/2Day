using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	private PlayerWeaponChange playerWeaponChange;

	public string playerName;

	public int playerHealth;
	public int score;
	public string weaponName;

	void Start(){
		playerWeaponChange = GetComponent<PlayerWeaponChange> ();
		playerName = PhotonNetwork.player.NickName;

		PhotonNetwork.player.SetStatus (playerHealth, 0, "HAND");
	}

	void Update(){
		if (playerWeaponChange.playerWeaponNum == -1)
			weaponName = "HAND";
		else {
			try{
				weaponName = playerWeaponChange.weapons [playerWeaponChange.playerWeaponNum].weaponName;
			}finally{
				weaponName = "HAND";
			}
		}
		PhotonNetwork.player.SetStatus (playerHealth, score, weaponName);
	}
}