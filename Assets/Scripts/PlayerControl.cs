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
    private bool isTurning = false;

    private void Start()
    {
        cam = Camera.main;
        cameraMovement = GameObject.FindObjectOfType<CameraMovement>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            PerformMovement();
            //PerformRotation();
        }
    }

    private void PerformMovement()
    {
        float _xAxis = Input.GetAxisRaw("Horizontal");
        float _zAxis = Input.GetAxisRaw("Vertical");
        if (_xAxis != 0 || _zAxis != 0)
        {
            Vector3 _moveVerticalVector = cam.transform.forward;
            _moveVerticalVector.y = 0;
            _moveVerticalVector = _zAxis * _moveVerticalVector;
            Vector3 _moveHorizontalVector = cam.transform.right;
            _moveHorizontalVector.y = 0;
            _moveHorizontalVector = _xAxis * _moveHorizontalVector;
            Vector3 _moveVector = (_moveHorizontalVector + _moveVerticalVector).normalized;
            Debug.Log(Vector3.Dot(transform.forward, _moveVector));
            if (Vector3.Dot(transform.forward, _moveVector)> 0.9999)
            {
                isTurning = false;
            }
            else
            {
                isTurning = true;
                transform.forward = Vector3.Lerp(transform.forward, _moveVector, Time.deltaTime * turnSpeed);
            }

            if (!isTurning)
            {
                playerController.Move(transform.forward * moveSpeed * Time.deltaTime);
            }
        }
       


        //if (Vector3.Dot(transform.forward,_moveVerticalVector)!=1)
        //{
        //Debug.Log("selam");
        //isTurning = true;
        //transform.forward = _moveVerticalVector;
        //}
        //else
        //{
        //isTurning = false;
        //}
        //if (!isTurning)
        //{
       
        
            
        //}
    }

    private void PerformRotation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (!cameraMovement.GetIsCamRotating())
            {
                Vector3 playerDirection = hitInfo.point - transform.position;
                playerDirection = playerDirection.normalized;
                playerDirection.y = 0;
                transform.forward = Vector3.Lerp(transform.forward, transform.forward + playerDirection, Time.deltaTime * turnSpeed);
            }
        }
    }
}
