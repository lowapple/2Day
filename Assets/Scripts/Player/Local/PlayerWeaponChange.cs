using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour {
	public PlayerWeapon[] weapons;
	public int playerWeaponNum = -1;

	void Start(){
		WeaponHide ();
	}

	void Update(){
		if (playerWeaponNum != 0) {
			WeaponHide ();
			weapons [playerWeaponNum].weapon.SetActive (true);
		} else {
			WeaponHide ();
		}
	}

	public void WeaponHide(){
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].weapon.SetActive (false);
		}
	}
}
