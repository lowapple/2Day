using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOnotherPlayerState : MonoBehaviour
{
    public Text playerName;
    public Text playerHealth;

    public void GetInfo(PhotonPlayer player)
    {
        playerName.text = player.NickName;

		PunPlayerData playerStatus = player.GetStatus ();
		if (playerStatus.health == -1) {
			Debug.Log ("player status null");
			player.SetStatus (100);
			playerStatus = player.GetStatus ();
		}
			
		playerHealth.text = playerStatus.health.ToString ();
    }
}