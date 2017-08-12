using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables

public class bl_RoomController : bl_PhotonHelper {

    [Separator("Inputs")]
    public KeyCode PauseKey = KeyCode.Escape;
    public KeyCode PauseMenuKey = KeyCode.M;
    public KeyCode ShopKey = KeyCode.Tab;
    
	[Separator("Player List")]
    public float UpdateListEach = 2f;
	public UIOnotherPlayerManager uiOnotherPlayerManager;

    [Separator("References")]
    public GameObject PauseMenuUI;

    public static bool Pause = false;
    protected bool Lock = true;
    protected bool m_ShowPlayerList = false;
	public UIStoreManager uiStoreManager;

    // private List<GameObject> CachePlayerList = new List<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        Pause = false;
		uiStoreManager.gameObject.SetActive (false);

		InvokeRepeating("UpdatePlayerStatus", 1, UpdateListEach);
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

		// Playser Shop List
		if (Input.GetKeyDown (ShopKey)) {
			Pause = !Pause;
			bl_CoopUtils.LockCursor(!Pause);
			uiStoreManager.gameObject.SetActive (true);
		} else if (Input.GetKeyUp (ShopKey)) {
			Pause = !Pause;
			bl_CoopUtils.LockCursor(!Pause);
			uiStoreManager.gameObject.SetActive (false);
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
    public void UpdatePlayerStatus()
    {
        if (!isConnected)
            return;
		
		uiOnotherPlayerManager.UpdatePlayerStatus (PhotonNetwork.playerList);
    }

    public void LeaveRoom() { PhotonNetwork.LeaveRoom(); }
}