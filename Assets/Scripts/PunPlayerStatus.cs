using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PunPlayerStatus : MonoBehaviour
{
    public const string PlayerStatusHealth = "statusHealth";
}

public class PunPlayerData
{
	public int health;
}

public static class StatusExtensions
{
    public static void SetStatus(
        this PhotonPlayer player, 
        int newHealth)
    {
        Hashtable status = new Hashtable();
        status[PunPlayerStatus.PlayerStatusHealth] = newHealth;
        player.SetCustomProperties(status);
    }

	public static PunPlayerData GetStatus(this PhotonPlayer player)
    {
		PunPlayerData playerStatus = new PunPlayerData();
		object health;

		if (player.CustomProperties.TryGetValue (PunPlayerStatus.PlayerStatusHealth, out health)) {
			playerStatus.health = (int)health;
		} else {
			Debug.Log ("Player Status Health is null");
			playerStatus.health = -1;
		}
       
		return playerStatus;
    }
}