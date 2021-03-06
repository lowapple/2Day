﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bl_GameController : bl_PhotonHelper {

    public static bool isPlaying = false;
    public static int m_ViewID = -1;
    [HideInInspector] public GameObject m_Player { get; set; }
    public GameObject PlayerPrefab = null;
    public string ReturnScene;
    private static List<bl_SpawnPoint> SpawnPoint = new List<bl_SpawnPoint>();
    public SpawnType m_SpawnType = SpawnType.Random;
    [Space(7)]
    public GameObject RoomCamera = null;
    public Image BlackBg = null;

    void Awake()
    {
        if (!isConnected || PhotonNetwork.room == null)
        {
            bl_UtilityHelper.LoadLevel(0);
            return;
        }
        StartCoroutine(FadeIn(1.5f));  

        PhotonNetwork.isMessageQueueRunning = true;
        if (PhotonNetwork.isMasterClient)
            PhotonNetwork.room.SendRoomState(true);

		SpawnPlayer ();
    }
		
    public void SpawnPlayer()
    {
        if (PlayerPrefab == null)
        {
            Debug.Log("Player Prefabs I was not assigned yet!");
            return;
        }

        if (m_Player != null)
            NetworkDestroy(m_Player);

        Vector3 p = Vector3.zero;
        Quaternion r = Quaternion.identity;

        GetSpawnPoint(out p, out r);

        m_Player = PhotonNetwork.Instantiate(PlayerPrefab.name,p,r, 0);
        m_ViewID = m_Player.GetViewID();

        if (RoomCamera != null)
            RoomCamera.SetActive(false);
        bl_CoopUtils.LockCursor(true);
        isPlaying = true;

        //Send a new log information
        string logText = LocalName + " joined to game";
		Debug.Log (logText);
    }

    void OnDisable()
    {
        isPlaying = false;
    }

    private int currentSpawnPoint = 0;
    private void GetSpawnPoint(out Vector3 position, out Quaternion rotation)
    {
            if (SpawnPoint.Count <= 0)
            {
                Debug.LogWarning("Doesn't have spawn point in scene");
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }

            if (m_SpawnType == SpawnType.Random)
            {

                int random = Random.Range(0, SpawnPoint.Count);
                Vector3 s = Random.insideUnitSphere * SpawnPoint[random].SpawnRadius;
                Vector3 pos = SpawnPoint[random].transform.position + new Vector3(s.x, 0, s.z);

                position = pos;
                rotation = SpawnPoint[random].transform.rotation;
            }
            else if (m_SpawnType == SpawnType.RoundRobin)
            {
                if (currentSpawnPoint >= SpawnPoint.Count) { currentSpawnPoint = 0; }
                Vector3 s = Random.insideUnitSphere * SpawnPoint[currentSpawnPoint].SpawnRadius;
                Vector3 v = SpawnPoint[currentSpawnPoint].transform.position + new Vector3(s.x, 0, s.z);
                currentSpawnPoint++;

                position = v;
                rotation = SpawnPoint[currentSpawnPoint].transform.rotation;
            }
            else
            {
                position = Vector3.zero;
                rotation = Quaternion.identity;
            }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    public static void RegisterSpawnPoint(bl_SpawnPoint point)
    {
        SpawnPoint.Add(point);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    public static void UnRegisterSpawnPoint(bl_SpawnPoint point)
    {
        SpawnPoint.Remove(point);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float t)
    {
        if (BlackBg == null)
            yield return null;

        BlackBg.gameObject.SetActive(true);
        Color c = BlackBg.color;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            c.a = t;
            BlackBg.color = c;
            yield return null;
        }
        BlackBg.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut(float t)
    {
        if (BlackBg == null)
            yield return null;
        BlackBg.gameObject.SetActive(true);
        Color c = BlackBg.color;
        while (c.a < t)
        {
            c.a += Time.deltaTime;
            BlackBg.color = c;
            yield return null;
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        if(m_Player != null)
        {  
            PhotonNetwork.Destroy(m_Player);
        }
        bl_CoopUtils.LoadLevel(ReturnScene);
    }
		
    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);

        //Send a new log information
        string logText = otherPlayer.NickName + " Left of room";
        //Send this as local because this function is already call in other players in room.
        bl_LogInfo inf = new bl_LogInfo(logText, Color.red,true);
        bl_EventHandler.OnLogMsnEvent(inf);
        //Debug.Log(otherPlayer.name + " Left of room");
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        }
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        //Send a new log information
        string logText = "We have a new Master Client: " + newMasterClient.NickName;
        //Send this as local because this function is already call in other players in room.
        bl_LogInfo inf = new bl_LogInfo(logText, Color.yellow, true);
        bl_EventHandler.OnLogMsnEvent(inf);
    }

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        bl_EventHandler.OnPhotonInstantiate(info);
    }

    [System.Serializable]
    public enum SpawnType
    {
        Random,
        RoundRobin,
    }
}