using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera; // Assign the third-person camera in the Inspector
    private Camera firstPersonCamera; // First-person camera inside the player prefab
    private GameObject fishingRod; // Fishing rod inside the player prefab

    public static bool isFirstPerson = false; // Track the current camera state

    void Start()
    {
        // Find the spawned player dynamically
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Get the First-Person Camera dynamically
        if (player != null)
        {
            firstPersonCamera = player.transform.Find("FPCamera").GetComponent<Camera>();
            fishingRod = player.transform.Find("FishingRod").gameObject; // Adjust this name if needed

            // Attach Cinemachine to follow the player
            thirdPersonCamera.Follow = player.transform;
            thirdPersonCamera.LookAt = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }

        // Set default view to first-person
        isFirstPerson = true;
        firstPersonCamera.enabled = true;
        thirdPersonCamera.enabled = false;
        fishingRod.SetActive(true);
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
            fishingRod.SetActive(true);
        }
        else
        {
            // Switch to third-person camera
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
            fishingRod.SetActive(false);
        }
    }
}
