using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCam;
    private bool isCamRotating = false;
    private float zoomLevel = 1f;
    private CinemachineFreeLook.Orbit[] originalOrbits;


    [SerializeField] private float zoomSpeed;
    [SerializeField] private float yAxisRotateSpeed;
    [SerializeField] private float xAxisRotateSpeed;
    private void Start()
    {
        PlayerSetup.OnLocalPlayerCreated += HandlePlayerCreated;
        freeLookCam.m_XAxis.m_MaxSpeed = yAxisRotateSpeed;
        freeLookCam.m_YAxis.m_MaxSpeed = xAxisRotateSpeed;
        originalOrbits = new CinemachineFreeLook.Orbit[freeLookCam.m_Orbits.Length];
        for (int i = 0; i < originalOrbits.Length; i++)
        {
            originalOrbits[i].m_Height = freeLookCam.m_Orbits[i].m_Height;
            originalOrbits[i].m_Radius = freeLookCam.m_Orbits[i].m_Radius;
        }
    }
    private void HandlePlayerCreated(Transform _target)
    {
        freeLookCam.Follow = _target;
        freeLookCam.LookAt = _target;
    }
    private void Update()
    {
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
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoomLevel = Mathf.Lerp(zoomLevel, zoomLevel - Input.GetAxis("Mouse ScrollWheel"), Time.deltaTime * zoomSpeed);
            zoomLevel = Mathf.Clamp(zoomLevel, 0.5f, 1f);
            for (int i = 0; i < freeLookCam.m_Orbits.Length; i++)
            {
                freeLookCam.m_Orbits[i].m_Height = originalOrbits[i].m_Height * zoomLevel;
                freeLookCam.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * zoomLevel;
            }
        }
    }
    public bool GetIsCamRotating()
    {
        return isCamRotating;
    }
}
