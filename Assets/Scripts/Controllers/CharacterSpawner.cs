using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    // Prefabs for Male and Female characters
    public GameObject malePrefab;
    public GameObject femalePrefab;

    // Spawn position
    public Vector3 spawnPosition = Vector3.zero; // Default position for spawning

    void Start()
    {
        // Read saved selection from PlayerPrefs (default is Male)
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 1); // Default to Male (1)

        // Spawn only the selected character
        if (selectedCharacter == 1) // Male
        {
            Instantiate(malePrefab, spawnPosition, Quaternion.identity); // Spawn Male prefab
        }
        else // Female
        {
            Instantiate(femalePrefab, spawnPosition, Quaternion.identity); // Spawn Female prefab
        }
    }
}
