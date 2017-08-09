using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ZombieSync : bl_PhotonHelper
{
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

    private void Awake()
    {
        if (!PhotonNetwork.connected)
            Destroy(this);

        m_PositionControl = new PhotonTransformViewPositionControl(m_PositionModel);
        m_RotationControl = new PhotonTransformViewRotationControl(m_RotationModel);
        m_ScaleControl = new PhotonTransformViewScaleControl(m_ScaleModel);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        m_PositionControl.OnPhotonSerializeView(transform.position, stream, info);
        m_RotationControl.OnPhotonSerializeView(transform.rotation, stream, info);
        m_ScaleControl.OnPhotonSerializeView(transform.localScale, stream, info);

        if (stream.isWriting)
        {

        }
        else
        {

        }
    }

    private void Update()
    {
        if(photonView == null || isMine == true || isConnected == false)
        {
            return;
        }
        
        UpdateRotation();
        UpdatePosition();
        UpdateScale();
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
