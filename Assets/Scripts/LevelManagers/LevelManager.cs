using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelManager : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public TextMeshProUGUI ResultText;
  public GameObject gameOverPanel;
  void StopSceneAndFocus()
  {
    // Pause the game
    Time.timeScale = 0;

    // Disable other scene objects (optional)
    DisableSceneObjects();

    Cursor.lockState = CursorLockMode.None; // Locks cursor to the center of the screen
    Cursor.visible = true; // Hides the cursor
  }

  void ToggleScriptState<T>(bool enabled) where T : MonoBehaviour
  {
    var controller = FindObjectOfType<T>();
    if (controller != null)
    {
      controller.enabled = enabled;
    }
  }

  void DisableSceneObjects()
  {
    ToggleScriptState<PlayerController>(false);
    ToggleScriptState<CameraSwitch>(false);
    ToggleScriptState<FishingRodSfx>(false);

    // Disable animations
    var animators = FindObjectsOfType<Animator>();
    foreach (var animator in animators)
    {
      animator.enabled = false;
    }
  }
  public void ResumeScene()
  {
    // Unpause the game
    Time.timeScale = 1;

    // Hide the panel
    gameOverPanel.SetActive(false);

    // Re-enable scene objects
    EnableSceneObjects();
  }
  void EnableSceneObjects()
  {
    ToggleScriptState<PlayerController>(true);
    ToggleScriptState<CameraSwitch>(true);
    ToggleScriptState<FishingRodSfx>(true);

    // Re-enable animations if disabled
    var animators = FindObjectsOfType<Animator>();
    foreach (var animator in animators)
    {
      animator.enabled = true;
    }
  }
  public void ToggleGameOver()
  {
    ScoreText.text = UIManager.points.ToString();
    gameOverPanel.SetActive(true);

    StopSceneAndFocus();
    if (UIManager.points > 0)
    {
      Debug.Log("You Win");
      ResultText.text = "You Win";
    }
    else
    {
      Debug.Log("Game Over");
      ResultText.text = "Game Over";
    }
  }
}