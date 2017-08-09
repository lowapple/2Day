using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : bl_PhotonHelper {
	public int playerWeaponNum = -1;
	private PlayerWeaponChange playerWeaponChange;

	void Start(){
		if (!isMine) {
			this.enabled = false;
		} else {
			playerWeaponChange = GetComponent<PlayerWeaponChange> ();
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			playerWeaponNum = 0;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){
			playerWeaponNum = 1;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3)){
			playerWeaponNum = 2;
		}

		if (playerWeaponChange != null)
			playerWeaponChange.playerWeaponNum = playerWeaponNum;
	}
}