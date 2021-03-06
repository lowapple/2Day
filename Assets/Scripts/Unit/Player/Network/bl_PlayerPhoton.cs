﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bl_PlayerPhoton : UnitPhoton {

    [Header("when the player is our disable these scripts")]
    public List<MonoBehaviour> Local_DisabledScripts = new List<MonoBehaviour>();
    [Header("when the player is Not our disable these scripts")]
    public List<MonoBehaviour> Remote_DisabledScripts = new List<MonoBehaviour>();
    [Header("when the player is our disable these GO")]
    public List<GameObject> Local_DesactiveObjects = new List<GameObject>();
    [Header("when the player is Not our disable these GO")]
    public List<GameObject> Remote_DesactiveObjects = new List<GameObject>();

    public const string PlayerTag = "Player";
    public const string RemoteTag = "NetworkPlayer";

    void Awake()
    {
#if UNITY_EDITOR
        bool found = false;
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(RemoteTag))
            {
                found = true;
            }
        }
        if (!found) { Debug.LogError("Please add the tag: <color=#4ECDC4>" + RemoteTag + "</color> in the tag list"); }
#endif
        if (isMine)
        {
            LocalPlayer(PlayerTag, Local_DisabledScripts, Local_DesactiveObjects);
        }
        else
        {
            NetworkPlayer();
        }
    }
    /// <summary>
    /// We call this function only if we are Remote player
    /// </summary>
    public void NetworkPlayer()
    {
        foreach (MonoBehaviour script in Remote_DisabledScripts)
        {
            Destroy(script);
        }
        foreach (GameObject obj in Remote_DesactiveObjects)
        {
            obj.SetActive(false);
        }
        this.gameObject.tag = RemoteTag;
    }
}