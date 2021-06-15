using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField] private CharacterController playerController;
    private Animator anim;
    private float moveSpeed = 4f;
    private float turnSpeed = 0.1f;
    
    private CameraMovement cameraMovement;
    private Camera cam;
   
    private bool isTurning = false;
    float turnSmoothVelocity;
    private void Start()
    {
        cam = Camera.main;
        cameraMovement = GameObject.FindObjectOfType<CameraMovement>();
        anim = GetComponent<NetworkAnimator>().GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            //PerformMovement();
            //PerformRotation();
            PerformMovement2();
        }
    }

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
            Debug.Log(transform.forward.normalized+ "    " + _moveVector.normalized);
            if (Vector3.Dot(transform.forward, _moveVector)> 0.999)
            //if (transform.forward == _moveVector)
            {
                Debug.Log("donme");
                isTurning = false;
            }
            else
            {
                Debug.Log("don");
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
