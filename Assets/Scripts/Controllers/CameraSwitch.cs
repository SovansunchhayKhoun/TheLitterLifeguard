using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera; // Assign the third-person camera
    public CinemachineVirtualCamera firstPersonCamera; // Assign the first-person camera
    public static bool isFirstPerson = false; // Track the current camera state

    void Start()
    {
        isFirstPerson = true;
        firstPersonCamera.Priority = 10;
        thirdPersonCamera.Priority = 0;
    }

    void Update()
    {
        // Toggle camera POV when pressing the 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCamera();
        }
    }

    private void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
        {
            // Switch to first-person camera
            firstPersonCamera.Priority = 10;
            thirdPersonCamera.Priority = 0;
        }
        else
        {
            // Switch to third-person camera
            firstPersonCamera.Priority = 0;
            thirdPersonCamera.Priority = 10;
        }
    }
}
