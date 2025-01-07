using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
  public void ToStart()
  {
    SceneManager.LoadScene(SceneNameEnum.START_SCENE);
  }
  public void ToAppearance()
  {
    SceneManager.LoadScene(SceneNameEnum.APPEARANCE_SCENE);
  }
  public void ToLevel()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_SCENE);
  }
  public void ToLevel1()
  {
    SceneManager.LoadScene(SceneNameEnum.LEVEL_1);
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
