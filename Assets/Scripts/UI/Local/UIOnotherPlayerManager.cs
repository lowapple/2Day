using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOnotherPlayerManager : MonoBehaviour 
{
	public GridLayoutGroup gridLayoutGroup;
	public GameObject playerStatusPrefab;
	public List<UIOnotherPlayerState> uiPlayerStatus;
	public List<PhotonPlayer> uiPlayers;

	void Awake(){
		uiPlayerStatus = new List<UIOnotherPlayerState> ();
		uiPlayers = new List<PhotonPlayer> ();
	}

	public void UpdatePlayerStatus(PhotonPlayer[] players){
		if (uiPlayerStatus.Count != players.Length - 1) {
			// Player Length
			if (uiPlayerStatus.Count > 0) {
				foreach (UIOnotherPlayerState g in uiPlayerStatus) {
					Destroy (g.gameObject);
				}
			}

			for (int i = 0; i < players.Length; i++) {
				var player = players [i];

				if (player.NickName.CompareTo (PhotonNetwork.player.NickName) != 0) {
					GameObject r = Instantiate (playerStatusPrefab) as GameObject;
					UIOnotherPlayerState status = r.GetComponent<UIOnotherPlayerState> ();
					uiPlayerStatus.Add (status);
					uiPlayers.Add (player);

					status.gameObject.SetActive (true);
					status.GetInfo (player);
					r.transform.SetParent (gridLayoutGroup.transform);
				}
			}
		}

		for (int i = 0; i < uiPlayers.Count; i++) {
			uiPlayerStatus [i].GetInfo (uiPlayers [i]);
		}
	}
}