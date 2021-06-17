using UnityEngine;
using Mirror;
using System;

public class PlayerSetup : NetworkBehaviour
{
    public static event Action<Transform> OnLocalPlayerCreated;
    [SerializeField] private Transform cameraFollowTransform;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        OnLocalPlayerCreated?.Invoke(cameraFollowTransform);
    }
    public override void OnStartClient()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }
}
