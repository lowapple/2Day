using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PunPlayerState : MonoBehaviour
{
    public const string PlayerStateHealth = "stateHealth";
	public const string PlayerStateWeaponName = "stateWeaponName";
	public const string PlayerStateScore = "stateScore";
}

public class PunPlayerStateData
{
	public string weaponName;
	public int score;
	public int health;
}

public static class StateExtensions
{
    public static void SetStatus(
        this PhotonPlayer player,
        int newHealth,
		int newScore,
		string newWeaponName)
    {
        Hashtable status = new Hashtable();
		status[PunPlayerState.PlayerStateHealth] = newHealth;
		status[PunPlayerState.PlayerStateScore] = newScore;
		status[PunPlayerState.PlayerStateWeaponName] = newWeaponName;
        player.SetCustomProperties(status);
    }

	public static PunPlayerStateData GetStatus(this PhotonPlayer player)
    {
		PunPlayerStateData playerStatus = new PunPlayerStateData();
		object health;
		object score;
		object weaponName;

		if (player.CustomProperties.TryGetValue (PunPlayerState.PlayerStateHealth, out health)) {
			playerStatus.health = (int)health;
		} else {
			Debug.Log ("Player State Health is null");
			playerStatus.health = -1;
		}

		if(player.CustomProperties.TryGetValue(PunPlayerState.PlayerStateScore, out score)){
			playerStatus.score = (int)score;
		}else{
			Debug.Log("Player State Score is null");
			playerStatus.score = -1;
		}

		if(player.CustomProperties.TryGetValue(PunPlayerState.PlayerStateWeaponName, out weaponName)){
			playerStatus.weaponName = (string)weaponName;
		}else{
			Debug.Log ("Player State Score is null");
			playerStatus.weaponName = "null";
		}
       
		return playerStatus;
    }
}