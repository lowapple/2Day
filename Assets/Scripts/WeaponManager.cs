using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
	private static WeaponManager instance;
	public static WeaponManager GetInstance {
		get {
			return instance;
		}
	}
	public PlayerWeapon[] playerWeapon;	


	void Awake(){
		if (instance == null)
			instance = this;
	}

	void Start(){
		for (int i = 0; i < playerWeapon.Length; i++)
			playerWeapon [i].localWeapon = Instantiate (playerWeapon [i].prefab, transform);
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