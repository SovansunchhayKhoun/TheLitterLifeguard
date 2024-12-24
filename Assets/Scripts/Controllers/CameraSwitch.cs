using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera; // Assign the third-person camera
    // public CinemachineVirtualCamera firstPersonCamera; // Assign the first-person camera
    public Camera firstPersonCamera; // Assign the first-person camera
    public static bool isFirstPerson = false; // Track the current camera state

    void Start()
    {
        isFirstPerson = true;
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
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
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
        }
        else
        {
            // Switch to third-person camera
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
        }
    }
}
