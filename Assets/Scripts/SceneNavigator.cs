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
    SceneManager.LoadScene(SceneNameEnum.START_SCREEN);
  }
  public void ToAppearance()
  {
    SceneManager.LoadScene(SceneNameEnum.APPEARANCE_SCREEN);
  }
  public void ToLevel()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_SCREEN);
  }
  public void ToLevel1()
  {
    SceneManager.LoadScene(SceneNameEnum.RAY_CASTING);
  }
  public void ToLevel2()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_2);
  }
  public void ToLevel3()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_3);
  }
  public static void ToSortingScene()
  {
    SceneManager.LoadScene(SceneNameEnum.SORTING_SCENE);
  }
}
