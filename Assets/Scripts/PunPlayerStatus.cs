using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PunPlayerStatus : bl_PhotonHelper
{
    public const string PlayerStatusHealth = "statusHealth";
    
    public object health = 0;
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

    public static PunPlayerStatus GetStatus(this PhotonPlayer player)
    {
        PunPlayerStatus playerStatus = new PunPlayerStatus();

        player.CustomProperties.TryGetValue(PunPlayerStatus.PlayerStatusHealth, out playerStatus.health);
        return playerStatus;
    }
}