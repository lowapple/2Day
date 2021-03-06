////////////////////////////////////////////////////////////////////////////////
//////////////////// bl_PlayerSync.cs///////////////////////////////////////////
////////////////////use this for the sincronizer pocision , rotation, states,/// 
///////////////////etc ...   via photon/////////////////////////////////////////
////////////////////////////////Briner Games////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class bl_PlayerSync : bl_PhotonHelper
{
    [HideInInspector]
    public string RemoteTeam;
    public string WeaponState;
    public Transform HeatTarget;
    public float SmoothingDelay = 8f;

    [SerializeField]
    PhotonTransformViewPositionModel m_PositionModel = new PhotonTransformViewPositionModel();
    [SerializeField]
    PhotonTransformViewRotationModel m_RotationModel = new PhotonTransformViewRotationModel();
    [SerializeField]
    PhotonTransformViewScaleModel m_ScaleModel = new PhotonTransformViewScaleModel();

    PhotonTransformViewPositionControl m_PositionControl;
    PhotonTransformViewRotationControl m_RotationControl;
    PhotonTransformViewScaleControl m_ScaleControl;

    bool m_ReceivedNetworkUpdate = false;
    [Space(5)]
    [Header("Necessary script")]
    public bl_PlayerAnimator m_PlayerAnimation;

    private bl_PlayerMovement Controller;
    private PlayerWeaponChange weaponChange;
    private PlayerWeaponController weaponController;
    private PlayerAttackController attackController;
    private GameObject CurrenGun;

#pragma warning disable 0414
    [SerializeField]
    bool ObservedComponentsFoldoutOpen = true;
#pragma warning disable 0414

    void Awake()
    {
        if (!PhotonNetwork.connected)
            Destroy(this);

        if (!this.isMine)
            if (HeatTarget.gameObject.activeSelf == false)
                HeatTarget.gameObject.SetActive(true);

        m_PositionControl = new PhotonTransformViewPositionControl(m_PositionModel);
        m_RotationControl = new PhotonTransformViewRotationControl(m_RotationModel);
        m_ScaleControl = new PhotonTransformViewScaleControl(m_ScaleModel);
        Controller = this.GetComponent<bl_PlayerMovement>();
        weaponController = this.GetComponent<PlayerWeaponController>();
        weaponChange = this.GetComponent<PlayerWeaponChange>();
        attackController = this.GetComponent<PlayerAttackController>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        m_PositionControl.OnPhotonSerializeView(transform.position, stream, info);
        m_RotationControl.OnPhotonSerializeView(transform.rotation, stream, info);
        m_ScaleControl.OnPhotonSerializeView(transform.localScale, stream, info);
        if (isMine == false && m_PositionModel.DrawErrorGizmo == true)
        {
            DoDrawEstimatedPositionError();
        }
        if (stream.isWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(gameObject.name);
            stream.SendNext(HeatTarget.position);
            stream.SendNext(HeatTarget.rotation);
            stream.SendNext(Controller.m_PlayerState);
            stream.SendNext(Controller.m_PlayerAttackState);
            stream.SendNext(Controller.grounded);
            stream.SendNext(Controller.vel);
            stream.SendNext(weaponController.playerWeaponNum);

            if (attackController.isAttack)
            {
                attackController.isAttack = false;
                stream.SendNext(true);
            }
            else
                stream.SendNext(false);

            for (int i = 0; i < 3; i++)
            {
                stream.SendNext(i);
                if (weaponChange.weapons[i] != null)
                    stream.SendNext(weaponChange.weapons[i].weaponName);
                else
                    stream.SendNext("null");
            }
        }
        else
        {
            //Network player, receive data
            RemotePlayerName = (string)stream.ReceiveNext();
            HeadPos = (Vector3)stream.ReceiveNext();
            HeadRot = (Quaternion)stream.ReceiveNext();
            m_state = (PlayerState)stream.ReceiveNext();
            m_playerWeaponState = (PlayerWeaponState)stream.ReceiveNext();
            m_grounded = (bool)stream.ReceiveNext();
            NetVel = (Vector3)stream.ReceiveNext();
            weaponNum = (int)stream.ReceiveNext();
            isAttack = (bool)stream.ReceiveNext();
            if (isAttack)
                m_PlayerAnimation.AnimationAttack();

            for (int i = 0; i < 3; i++)
            {
                int idx = (int)stream.ReceiveNext();
                string idxName = (string)stream.ReceiveNext();
                weaponChange.LocalWeaponChange(idx, idxName);
            }

            m_ReceivedNetworkUpdate = true;
        }
    }

    private Vector3 HeadPos = Vector3.zero;// Head Look to
    private Quaternion HeadRot = Quaternion.identity;
    private PlayerState m_state;
    private PlayerWeaponState m_playerWeaponState;
    private bool m_grounded;
    private string RemotePlayerName = string.Empty;
    private int weaponNum;
    private Vector3 NetVel;
    private bool isAttack = false;

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        ///if the player is not ours, then
        if (photonView == null || isMine == true || isConnected == false)
        {
            return;
        }

        UpdatePosition();
        UpdateRotation();
        UpdateScale();

        //Get information from other client
        this.HeatTarget.position = Vector3.Lerp(this.HeatTarget.position, HeadPos, Time.deltaTime * this.SmoothingDelay);
        this.HeatTarget.rotation = HeadRot;
        m_PlayerAnimation.m_PlayerState = m_state;//send the state of player local for remote animation*/
        m_PlayerAnimation.m_PlayerWeaponState = m_playerWeaponState;
        m_PlayerAnimation.grounded = m_grounded;
        m_PlayerAnimation.velocity = NetVel;

        weaponChange.playerWeaponNum = weaponNum;

        if (this.gameObject.name != RemotePlayerName)
            gameObject.name = RemotePlayerName;
    }


    public void IsFire(string m_type, float t_spread)
    {
        photonView.RPC("FireSync", PhotonTargets.Others, new object[] { m_type, t_spread });
    }

    void UpdatePosition()
    {
        if (m_PositionModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.position = m_PositionControl.UpdatePosition(transform.position);
    }

    void UpdateRotation()
    {
        if (m_RotationModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.rotation = m_RotationControl.GetRotation(transform.rotation);
    }

    void UpdateScale()
    {
        if (m_ScaleModel.SynchronizeEnabled == false || m_ReceivedNetworkUpdate == false)
        {
            return;
        }

        transform.localScale = m_ScaleControl.GetScale(transform.localScale);
    }

    void DoDrawEstimatedPositionError()
    {
        Vector3 targetPosition = m_PositionControl.GetNetworkPosition();

        Debug.DrawLine(targetPosition, transform.position, Color.red, 2f);
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.green, 2f);
        Debug.DrawLine(targetPosition, targetPosition + Vector3.up, Color.red, 2f);
    }

    public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
    {
        m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
    }
}