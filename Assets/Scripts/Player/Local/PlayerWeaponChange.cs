using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour {
	private const int PLAYER_MAX_WEAPON = 3;

	// current Player Weapon
	public int playerWeaponNum = -1;
	public int prevPlayerWeaponNum = -1;


	public PlayerWeapon[] weapons = new PlayerWeapon[PLAYER_MAX_WEAPON];
	private bl_PlayerAnimator playerAnimator;

	void Start(){
		playerAnimator = GetComponent<bl_PlayerAnimator> ();
		// --- Fix ---
		weapons[0] = WeaponManager.GetInstance.GetWeapon("Axe");
		weapons [1] = WeaponManager.GetInstance.GetWeapon ("Hammer");

		WeaponHide ();
	}

	void Update(){
		if (playerWeaponNum != -1 && weapons[playerWeaponNum] != null) {
			if (prevPlayerWeaponNum != playerWeaponNum) {
				prevPlayerWeaponNum = playerWeaponNum;

				WeaponHide ();
				weapons [playerWeaponNum].localWeapon.SetActive (true);
				weapons [playerWeaponNum].networkWeapon.SetActive (true);
			}
			// playerAnimator.m_PlayerWeaponState = weapons [playerWeaponNum].weaponState;
		} else {
			WeaponHide ();
		}
	}

	public void WeaponHide(){
		for (int i = 0; i < PLAYER_MAX_WEAPON; i++) {
			if (weapons [i] != null) {
				if(weapons[i].networkWeapon != null)
					weapons [i].networkWeapon.SetActive (false);
				if(weapons[i].localWeapon != null)
					weapons [i].localWeapon.SetActive (false);
			}
		}
	}
}