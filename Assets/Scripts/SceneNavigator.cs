using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
  public static void ToStart()
  {
    SceneManager.LoadScene(SceneNameEnum.START_SCENE);
  }
  public static void ToAppearance()
  {
    SceneManager.LoadScene(SceneNameEnum.APPEARANCE_SCENE);
  }
  public static void ToLevel()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_SCENE);
  }
  public static void ToLevel1()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_1);
  }
  public static void ToLevel2()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_2);
  }
  public static void ToLevel3()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_3);
  }
  public static void ToLevel1SortingScene()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL1_SORTING_SCENE);
  }
  public static void ToLevel2SortingScene()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL2_SORTING_SCENE);
  }
  public static void ToLevel3SortingScene()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL3_SORTING_SCENE);
  }
}
