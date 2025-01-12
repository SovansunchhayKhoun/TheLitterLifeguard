using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Level1Manager : LevelManager
{
  public Text timer;
  public static float time;
  public void Start()
  {
    UpdateTimerDisplay();
    InitGame();
    // StartCoroutine(Countdown());
  }
  protected override void Update()
  {
    base.Update();
    Countdown();
  }

  private void InitGame()
  {
    time = 3f;
    UpdateTimerDisplay();
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

  private void UpdateTimerDisplay()
  {
    int minutes = (int)(time / 60);
    int seconds = (int)(time % 60);
    timer.text = $"{minutes}mn{seconds:D2}s";  // D2 formats seconds to always show 2 digits
  }
}
