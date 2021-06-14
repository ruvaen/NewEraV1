using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
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
        if (target == null)
        {
            return;
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
