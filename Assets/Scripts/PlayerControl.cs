using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum MovementState { Idle, Walking, Running}
public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private PlayerAnimation animationHandler;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 10f;
    
    private Camera cam;
    private Vector3 movementDirection;
    private MovementState movementState;
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
            float _lookToForwardAngle = Vector3.SignedAngle(transform.forward, movementDirection, transform.up);
            animationHandler.HandleMovement(movementState, _lookToForwardAngle, movementDirection);
            Debug.Log(Vector3.Dot(transform.forward.normalized,movementDirection.normalized)+"     "+ Vector3.Dot(transform.right.normalized, movementDirection.normalized));
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
            movementState = MovementState.Running;
            playerController.Move(movementDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            movementState = MovementState.Idle;
        }
        
    }
    private void PerformRotation()
    {
        Quaternion _targetRotation = transform.rotation;
        if (Input.GetMouseButton(0))
        {
            if(movementDirection.magnitude > 0)
            {
                movementState = MovementState.Walking;
            }
            Ray _ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitInfo;
            if (Physics.Raycast(_ray, out _hitInfo, Mathf.Infinity, groundLayer))
            {
                Vector3 _mouseDirection = (_hitInfo.point - transform.position).normalized;
                _mouseDirection.y = 0;
                _targetRotation = Quaternion.LookRotation(_mouseDirection, transform.up);
            }
        }
        else if(!Input.GetMouseButton(0) && movementDirection.magnitude > 0)
        {
            _targetRotation = Quaternion.LookRotation(movementDirection, transform.up);
        }

        if (_targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
        }
    }
    
}
