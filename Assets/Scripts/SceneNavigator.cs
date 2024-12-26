using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
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
