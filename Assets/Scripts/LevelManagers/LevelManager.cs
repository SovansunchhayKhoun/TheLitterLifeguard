using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelManager : MonoBehaviour
{
  public static float time;
  public Text TimerText;
  public TextMeshProUGUI ScoreText;
  public TextMeshProUGUI ResultText;
  public GameObject GameOverPanel;
  public GameObject SettingPanel;

  private bool isOpen = false;
  protected virtual void Awake()
  {
    isOpen = !isOpen;
    ToggleMenu();
  }
  protected virtual void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      ToggleMenu();
    }
    Countdown();
  }
  protected virtual void Start()
  {
    UpdateTimerDisplay();
    time = 30f;
  }
  void ToggleMenu()
  {
    isOpen = !isOpen;
    SettingPanel.SetActive(isOpen);

    if (isOpen)
    {
      Time.timeScale = 0;
      Cursor.lockState = CursorLockMode.None;
    }
    else
    {
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
  private void UpdateTimerDisplay()
  {
    int minutes = (int)(time / 60);
    int seconds = (int)(time % 60);
    TimerText.text = $"{minutes}mn{seconds:D2}s";  // D2 formats seconds to always show 2 digits
  }
  private void Countdown()
  {
    if (time > 0)
    {
      // yield return new WaitForSeconds(1f);
      time -= Time.deltaTime;
      UpdateTimerDisplay();
    }
    else
    {
      // Game Over when timer hits 0
      ToggleGameOver();
    }
  }
  public void Resume()
  {
    Debug.Log("Resume");
    isOpen = false;
    SettingPanel.SetActive(false);
    Time.timeScale = 1;
    Cursor.lockState = CursorLockMode.Locked;
  }
  public void Exit()
  {
    // SceneNavigator.ToLevel();
    Time.timeScale = 1;
    Application.Quit();
  }

  public void ToggleGameOver()
  {
    ScoreText.text = UIManager.points.ToString();
    GameOverPanel.SetActive(true);

    StopSceneAndFocus();
    if (UIManager.points > 0)
    {
      ResultText.text = "You Win";
    }
    else
    {
      ResultText.text = "Game Over";
    }
  }
  void StopSceneAndFocus()
  {
    // Pause the game
    Time.timeScale = 0;

    // Disable other scene objects (optional)
    DisableSceneObjects();

    Cursor.lockState = CursorLockMode.None; // Locks cursor to the center of the screen
    Cursor.visible = true; // Hides the cursor
  }

  private void ToggleScriptState<T>(bool enabled) where T : MonoBehaviour
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
}