using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour {
	public PlayerWeapon[] weapons;
	public int playerWeaponNum = -1;
	private bl_PlayerAnimator playerAnimator;

	void Start(){
		playerAnimator = GetComponent<bl_PlayerAnimator> ();
		WeaponHide ();
	}

	void Update(){
		if (playerWeaponNum != 0) {
			WeaponHide ();
			weapons [playerWeaponNum].weapon.SetActive (true);
			playerAnimator.m_PlayerWeaponState = weapons [playerWeaponNum].weaponState;
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
