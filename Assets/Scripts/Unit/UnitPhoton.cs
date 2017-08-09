using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPhoton : bl_PhotonHelper
{
    public void LocalPlayer(string PlayerTag, List<MonoBehaviour> Local_DisabledScripts, List<GameObject> Local_DesactiveObjects)
    {
        gameObject.name = PhotonNetwork.playerName;
        foreach (MonoBehaviour script in Local_DisabledScripts)
        {
            Destroy(script);
        }
        foreach (GameObject obj in Local_DesactiveObjects)
        {
            obj.SetActive(false);
        }
        this.gameObject.tag = PlayerTag;
    }

    public bool isLocalPlayer
    {
        get
        {
            return isMine;
        }
    }
}
