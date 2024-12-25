using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
  // Public variables to assign prefabs for Male and Female characters
    public GameObject malePrefab;
    public GameObject femalePrefab;

    // Variable to store character selection
    public static bool isMale = true; // Default to Male

    // Save the selection (Male/Female)
    public void SelectMale()
    {
        isMale = true;
        Debug.Log("Male Selected");
    }

    public void SelectFemale()
    {
        isMale = false;
        Debug.Log("Female Selected");
    }
  public void ToStart()
  {
    SceneManager.LoadScene("StartScreen");
  }
  public void ToAppearance()
  {
    SceneManager.LoadScene("AppearanceScreen");
  }
  public void ToLevel()
  {
    SceneManager.LoadScene("LevelScreen");
  }
  public void ToGame()
  {
    SceneManager.LoadScene("DemoScene 1");
  }

}
