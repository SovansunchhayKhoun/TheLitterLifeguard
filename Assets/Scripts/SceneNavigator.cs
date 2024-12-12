using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
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

}
