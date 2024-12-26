using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppearanceSelector : MonoBehaviour
{
    // Buttons for selecting characters
    public Button maleButton;
    public Button femaleButton;

    // Track selection
    public static bool isMale = true; // Default to male

    void Start()
    {
        // Add listeners for clicks
        maleButton.onClick.AddListener(() => SelectCharacter(true));  // Male
        femaleButton.onClick.AddListener(() => SelectCharacter(false)); // Female
    }

    // Select character and immediately start the game
    public void SelectCharacter(bool male)
    {
        // Set selection
        isMale = male;
        PlayerPrefs.SetInt("SelectedCharacter", male ? 1 : 0); // Save as 1 (Male) or 0 (Female)
        PlayerPrefs.Save(); // Ensure it gets saved immediately

        // Debug selection
        Debug.Log(male ? "Male Selected" : "Female Selected");

        // Load the game scene directly
        SceneManager.LoadScene("RayCasting");  // Adjust scene name as needed
    }
}
