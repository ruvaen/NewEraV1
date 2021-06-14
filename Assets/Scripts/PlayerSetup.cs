using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerSetup : NetworkBehaviour
{
    public static event Action<Transform> OnPlayerCreated;
    [SerializeField] private Transform cameraFollowTransform;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        OnPlayerCreated?.Invoke(cameraFollowTransform);
    }

}
