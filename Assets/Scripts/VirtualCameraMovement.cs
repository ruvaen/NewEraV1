using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualLookCam;
    private void Start()
    {
        PlayerSetup.OnPlayerCreated += HandlePlayerCreated;
    }
    private void HandlePlayerCreated(Transform _target)
    {
        virtualLookCam.Follow = _target;
        virtualLookCam.LookAt = _target;
    }
}
