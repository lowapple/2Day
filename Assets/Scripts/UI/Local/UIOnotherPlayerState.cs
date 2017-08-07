using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOnotherPlayerState : MonoBehaviour
{
    private PunPlayerStatus playerStatus;

    public Text playerName;
    public Text playerHealth;

    void GetInfo(PhotonPlayer player)
    {
        playerStatus = player.GetStatus();
        playerName.text = player.NickName;
    }

    void Update()
    {
        playerHealth.text = (string)playerStatus.health;
    }
    
}