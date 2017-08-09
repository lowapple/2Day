using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {
	public PlayerWeaponState weaponState;
	public string weaponName;

	public GameObject prefab;
	public GameObject localWeapon;
	public GameObject networkWeapon;
}