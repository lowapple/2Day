using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : bl_PhotonHelper {
	public int playerWeaponNum = -1;

	void Start(){
		if (!isMine)
		{
			this.enabled = false;
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
	}
}