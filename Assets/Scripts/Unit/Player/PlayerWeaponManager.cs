using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour {
	public PlayerWeapon[] playerWeapon;	
	public GameObject localWeaponParent;

	void Start(){
		for (int i = 0; i < playerWeapon.Length; i++)
			playerWeapon [i].localWeapon = Instantiate (playerWeapon [i].prefab, localWeaponParent.transform);
	}

	public PlayerWeapon GetWeapon(string weaponName){
		for (int i = 0; i < playerWeapon.Length; i++) {
			if (playerWeapon [i].weaponName.CompareTo (weaponName) == 0) {
				return playerWeapon [i];
			}
		}
		return null;
	}
}