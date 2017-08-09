using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables

public class bl_RoomController : bl_PhotonHelper {

    [Separator("Inputs")]
    public KeyCode PauseKey = KeyCode.Escape;
    public KeyCode PauseMenuKey = KeyCode.M;
    public KeyCode PlayerListKey = KeyCode.Tab;
    
	[Separator("Player List")]
    public float UpdateListEach = 5f;
	public UIOnotherPlayerManager uiOnotherPlayerManager;

    [Separator("Ping Settings")]
    public float UpdatePingEach = 5f;
    /// <summary>
    /// Max Ping to show message alert.
    /// </summary>
    public int MaxPing = 500;
    public GameObject PingMsnUI;
    [Separator("References")]
    public GameObject PauseMenuUI;

    public static bool Pause = false;
    protected bool Lock = true;
    protected bool m_ShowPlayerList = false;

    // private List<GameObject> CachePlayerList = new List<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Pause = false;
        InvokeRepeating("UpdatePlayerStatus", 1, UpdateListEach);
        InvokeRepeating("UpdatePing", 1, UpdatePingEach);
        UpdatePing();
        UpdatePlayerList();
    }
    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        Inputs();
    }

    /// <summary>
    /// 
    /// </summary>
    void Inputs()
    {
        //Show / Hide Pause Menu.
        if (Input.GetKeyDown(PauseMenuKey))
        {
            PauseEvent();
        }
        //Lock / Unlock cursor
        if (Input.GetKeyDown(PauseKey))
        {
            Lock = false;
            bl_CoopUtils.LockCursor(Lock);  
        }
        if (Input.GetMouseButtonDown(0) && bl_GameController.isPlaying && !Pause && !Lock && !m_ShowPlayerList)
        {
            Lock = true;
            bl_CoopUtils.LockCursor(Lock);  
        }

        if(Input.GetMouseButtonDown(0) && !Pause && bl_GameController.isPlaying && !m_ShowPlayerList)
        {
            Lock = true;
            bl_CoopUtils.LockCursor(Lock);
        }
    }

    /// <summary>
    /// Show / Hide Pause Menu
    /// </summary>
    public void PauseEvent()
    {
        Pause = !Pause;
        bl_CoopUtils.LockCursor(!Pause);
        PauseMenuUI.SetActive(Pause);
    }
    /// <summary>
    /// 
    /// </summary>
    public void UpdatePlayerList()
    {
        if (!isConnected)
            return;
		
		uiOnotherPlayerManager.UpdatePlayerStatus (PhotonNetwork.playerList);
    }

    /// <summary>
    /// Update the player ping
    /// verify the state of ping
    /// </summary>
    void UpdatePing()
    {
        //Get ping from cloud
        int Ping = PhotonNetwork.GetPing();

        //Send ping to other player can access or see it.
        Hashtable PlayerPing = new Hashtable();
        PlayerPing.Add("Ping", Ping);
        PhotonNetwork.player.SetCustomProperties(PlayerPing);


        if (PingMsnUI != null)
        {
            //If ping mayor that max ping allowed
            if (Ping > MaxPing)
            {
                //Show alert mesagge
                //NOTE: you can write here your code for kick player if you want
                //when he has too ping.
                if (!PingMsnUI.activeSelf)
                {
                    PingMsnUI.SetActive(true);
                }
            }
            else
            {
                if (PingMsnUI.activeSelf)
                {
                    PingMsnUI.SetActive(false);
                }
            }
        }
    }

    public void LeaveRoom() { PhotonNetwork.LeaveRoom(); }
}