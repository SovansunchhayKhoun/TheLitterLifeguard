using UnityEngine;
using Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
    // Prefabs for Male and Female characters
    public GameObject malePrefab;
    public GameObject femalePrefab;

    // Camera references
    public CinemachineFreeLook thirdPersonCamera;  // The third-person camera (Cinemachine FreeLook)
    public Camera firstPersonCamera;  // The first-person camera

    // The spawned character (Male or Female)
    private GameObject currentCharacter;

    // Optionally set a spawn point for where the character should appear
    public Transform spawnPoint;

    void Start()
    {
        // Get the selected character (from PlayerPrefs, 1 = Male, 0 = Female)
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 1); // Default to Male (1)

        // Destroy any existing character before spawning a new one
        GameObject existingCharacter = GameObject.FindWithTag("Player");
        if (existingCharacter != null)
        {
            Destroy(existingCharacter); // Remove any previous character
        }

        // Determine spawn position (using spawnPoint or fallback logic)
        Vector3 spawnPosition = spawnPoint ? spawnPoint.position : GetGroundSpawnPosition();

        // Spawn the selected character
        if (selectedCharacter == 1) // Male
        {
            currentCharacter = Instantiate(malePrefab, spawnPosition, Quaternion.identity); // Spawn Male prefab
        }
        else // Female
        {
            currentCharacter = Instantiate(femalePrefab, spawnPosition, Quaternion.identity); // Spawn Female prefab
        }

        // Set the character tag to "Player" so the camera can follow it
        currentCharacter.tag = "Player";

        // Attach the First-Person Camera to the character
        firstPersonCamera.transform.SetParent(currentCharacter.transform);
        firstPersonCamera.transform.localPosition = new Vector3(0, 1.5f, 0);  // Adjust height above the character
        firstPersonCamera.transform.localRotation = Quaternion.identity;  // Keep the rotation aligned

        // Attach the Fishing Rod to the character's right hand (you need to adjust this if your character has a specific bone setup)
        AttachFishingRodToCharacter();

        // Set the third-person camera to follow the character
        SetCharacterFollow();
    }

    void Update()
    {
        // Ensure that the camera is following the current character
        SetCharacterFollow();
    }

    private void SetCharacterFollow()
    {
        // Ensure the third-person camera follows the character
        if (currentCharacter != null)
        {
            if (thirdPersonCamera != null)
            {
                thirdPersonCamera.Follow = currentCharacter.transform;  // Ensure camera follows the character
                thirdPersonCamera.LookAt = currentCharacter.transform;   // Ensure camera looks at the character
            }

            // Ensure the first-person camera follows the character's head
            if (firstPersonCamera != null)
            {
                firstPersonCamera.transform.position = currentCharacter.transform.position + new Vector3(0, 1.5f, 0);  // Adjust height above head
                firstPersonCamera.transform.rotation = currentCharacter.transform.rotation;
            }
        }
    }   


    private void AttachFishingRodToCharacter()
    {
        // Assuming you have a "RightHand" bone or empty game object in the character prefab
        Transform rightHand = currentCharacter.transform.Find("RightHand"); // Adjust this to your character's rig setup
        if (rightHand != null)
        {
            // Attach the fishing rod to the character's right hand
            GameObject fishingRod = currentCharacter.transform.Find("FishingRod").gameObject; // Make sure your fishing rod is part of the character prefab
            fishingRod.transform.SetParent(rightHand);
            fishingRod.transform.localPosition = Vector3.zero;  // Adjust this position if needed
            fishingRod.transform.localRotation = Quaternion.identity;  // Adjust rotation if needed
        }
        else
        {
            Debug.LogWarning("RightHand not found in character!");
        }
    }

    private Vector3 GetGroundSpawnPosition()
    {
        // Raycast down to find the ground (assuming ground is at Y=0 or higher)
        RaycastHit hit;
        Vector3 rayStartPosition = new Vector3(-65, 100, 30); // Set a higher point above the character's spawn location
        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            // Ensure that we account for the character's height after the raycast
            float characterHeight = 1.8f; // Adjust this to match the height of your character
            return new Vector3(hit.point.x, hit.point.y + characterHeight, hit.point.z); // Add height adjustment
        }
        else
        {
            return new Vector3(-65, 3, 30); // Fallback to default spawn if no ground detected
        }
    }

}
