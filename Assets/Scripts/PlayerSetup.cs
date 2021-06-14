using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerSetup : NetworkBehaviour
{
    public static event Action<Transform> OnPlayerCreated;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        OnPlayerCreated?.Invoke(transform);
    }

}
