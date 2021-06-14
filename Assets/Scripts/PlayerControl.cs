using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 20f;
    
    private CameraMovement cameraMovement;
    private Camera cam;



    private void Start()
    {
        cam = Camera.main;
        cameraMovement = cam.gameObject.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            PerformMovement();
            PerformRotation();
        }


    }

    private void PlayerShoot()
    {

    }
    private void PerformMovement()
    {
        float _xAxis = Input.GetAxisRaw("Horizontal");
        float _zAxis = Input.GetAxisRaw("Vertical");

        Vector3 _movement = (transform.forward * _zAxis) + (transform.right * _xAxis);
        playerController.Move(_movement.normalized * moveSpeed * Time.deltaTime);
    }

    private void PerformRotation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (!cameraMovement.IsRotating())
            {
                Vector3 playerDirection = hitInfo.point - transform.position;
                playerDirection = playerDirection.normalized;
                playerDirection.y = 0;
                transform.forward = Vector3.Lerp(transform.forward, transform.forward + playerDirection, Time.deltaTime * turnSpeed);
            }
        }
    }
}
