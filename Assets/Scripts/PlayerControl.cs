using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 10f;
    
    private Camera cam;
    private Vector3 movementDirection;
    private bool isMoving = false;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            PerformMovement();
            PerformRotation();
            //PerformMovement2();
        }
    }
    /*
    private void PerformMovement2()
    {
        float _horizontalAxis = Input.GetAxisRaw("Horizontal");
        float _verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(_horizontalAxis, 0f, _verticalAxis).normalized;
        Debug.Log(direction.magnitude);
        anim.SetFloat("running", direction.magnitude);
        if (direction.magnitude >= 0.1)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            cam.transform.forward = transform.forward;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            playerController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
    */
    private void PerformMovement()
    {
        float _xAxis = Input.GetAxisRaw("Horizontal");
        float _zAxis = Input.GetAxisRaw("Vertical");
        Vector3 _verticalDirectionVector = cam.transform.forward * _zAxis;
        _verticalDirectionVector.y = 0;
        Vector3 _horizontalDirectionVector = cam.transform.right * _xAxis;
        _horizontalDirectionVector.y = 0;
        movementDirection = (_verticalDirectionVector + _horizontalDirectionVector).normalized;
        if (movementDirection.magnitude > 0)
        {
            isMoving = true;
            playerController.Move(movementDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }
    private void PerformRotation()
    {
        Quaternion _targetRotation = transform.rotation;
        if (isMoving && !Input.GetMouseButton(0))
        {
            _targetRotation = Quaternion.LookRotation(movementDirection, transform.up);
        }
        else if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundLayer))
            {
                Vector3 _mouseDirection = (hitInfo.point - transform.position).normalized;
                _mouseDirection.y = 0;
                _targetRotation = Quaternion.LookRotation(_mouseDirection, transform.up);
            }
        }
        if (_targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
        }
    }
}
