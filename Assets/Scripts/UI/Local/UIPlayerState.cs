using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIPlayerState : MonoBehaviour {

	public Text Health;
	public Text WeaponName;
	public Text Score;

	void Update () {
		var playerData = PhotonNetwork.player.GetStatus ();
		if (playerData.health != -1) {
			Health.text = playerData.health.ToString ();
			WeaponName.text = playerData.weaponName.ToString ();
			Score.text = playerData.score.ToString ();
		}
	}
}
