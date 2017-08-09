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

		var playerStatus = player.GetStatus ();

		if (playerHealth == null)
			Debug.Log ("Player Health null");

		// playerHealth.text = (100).ToString ();
		// playerHealth.text = ((int)playerStatus.health).ToString ();
    }
}