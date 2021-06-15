using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCameraMovement : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float zoomSpeed = 100f;
    private Vector3 offsetVector = new Vector3(0, 10, -6);
    float maxZoomDistance = 20f;
    float minZoomDistance = 2f;

    private bool isRotating = false;
    public bool IsRotating()
    {
        return isRotating; 
    }

    private void Start()
    {
        PlayerSetup.OnPlayerCreated += OnPlayerCreated;
    }

    private void OnPlayerCreated(Transform _transform)
    {
        target = _transform;
    }





    private void LateUpdate()
    {
<<<<<<< Updated upstream:Assets/Scripts/CameraMovement.cs
        if (target == null)
        {
            return;
=======
        if (Input.GetMouseButton(1))
        {
            isCamRotating = true;
            freeLookCam.m_XAxis.m_InputAxisValue = Input.GetAxis("Mouse X");
            //freeLookCam.m_YAxis.m_InputAxisValue = Input.GetAxis("Mouse Y");
        }
        else
        {
            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            //freeLookCam.m_YAxis.m_InputAxisValue = 0f;
            isCamRotating = false;
>>>>>>> Stashed changes:Assets/Scripts/FreeLookCameraMovement.cs
        }

        transform.position = target.position + offsetVector;
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            float zoomCalculated = scroll * zoomSpeed;
            if (Vector3.Distance(transform.position, target.position) - zoomCalculated < maxZoomDistance && Vector3.Distance(transform.position, target.position) - zoomCalculated > minZoomDistance)
            {
                transform.Translate(transform.forward * zoomCalculated, Space.World);
                offsetVector = transform.position - target.position;
            }
        }

        transform.position = target.position + offsetVector; 
        transform.LookAt(target);

        if (Input.GetMouseButton(1))
        {
            isRotating = true;
            offsetVector = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetVector;

            transform.RotateAround(target.position, Vector3.up, 20 * Time.deltaTime);
        }
        else
        {
            isRotating = false;
        }
    }
}
