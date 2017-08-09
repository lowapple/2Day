using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponChange : MonoBehaviour {
	public PlayerWeaponManager playerWeaponManager;

	private const int PLAYER_MAX_WEAPON = 3;
	public int playerWeaponNum = -1;
	[HideInInspector]
	public int prevPlayerWeaponNum = -1;

	public PlayerWeapon[] weapons = new PlayerWeapon[PLAYER_MAX_WEAPON];

	void Start(){
		weapons [0] = playerWeaponManager.GetWeapon ("Axe");
		weapons [1] = playerWeaponManager.GetWeapon ("Hammer");
		weapons [2] = null;

		WeaponHide ();
	}

	// Local Weapon -> Network Weapon Change
	public void LocalWeaponChange(int weaponNum, string weaponName){
		if (weaponName == null)
			weapons [weaponNum] = null;
		else
			weapons [weaponNum] = playerWeaponManager.GetWeapon (weaponName);
	}

	void Update(){
		if (playerWeaponNum != -1 && weapons[playerWeaponNum] != null) {
			if (prevPlayerWeaponNum != playerWeaponNum) {
				prevPlayerWeaponNum = playerWeaponNum;
				WeaponHide ();
				weapons [playerWeaponNum].localWeapon.SetActive (true);
				weapons [playerWeaponNum].networkWeapon.SetActive (true);
			}
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