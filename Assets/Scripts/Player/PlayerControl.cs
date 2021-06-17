using UnityEngine;
using Mirror;

public enum MovementState { Idle, Walking, Running}
public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private CharacterController playerController;
    [SerializeField] private PlayerAnimation animationHandler;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float turnSpeed = 10f;
    
    private Camera cam;
    private Vector3 movementDirection;
    private MovementState movementState;
    private bool isLeftMouseButtonHeldDown = false;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            isLeftMouseButtonHeldDown = Input.GetMouseButton(0);
            PerformMovement();
            PerformRotation();
            animationHandler.HandleMovement(transform, movementDirection, movementState);
        }
    }
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
            float _speed = (isLeftMouseButtonHeldDown) ? walkSpeed : runSpeed;
            playerController.Move(movementDirection * _speed * Time.deltaTime);
        }
        else
        {
            movementState = MovementState.Idle;
        }
        
    }
    private void PerformRotation()
    {
        Quaternion _targetRotation = transform.rotation;
        if (isLeftMouseButtonHeldDown)
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
        else if(!isLeftMouseButtonHeldDown && movementDirection.magnitude > 0)
        {
            _targetRotation = Quaternion.LookRotation(movementDirection, transform.up);
        }

        if (_targetRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * turnSpeed);
        }
    }
    
}
