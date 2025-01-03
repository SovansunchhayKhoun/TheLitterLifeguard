using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Level1Manager : MonoBehaviour
{
  public Text timer;
  public static float time = 30f;
  public void Start()
  {
    UpdateTimerDisplay();
    InitGame();
    // StartCoroutine(Countdown());
  }
  void Update()
  {
    Countdown();
  }

  private void InitGame()
  {
    time = 30f;
    UpdateTimerDisplay();
  }

  private void Countdown()
  {
    {
      // yield return new WaitForSeconds(1f);
      time -= Time.deltaTime;
      UpdateTimerDisplay();
    }

    // Game Over when timer hits 0
    Debug.Log("Game over");
  }

  private void UpdateTimerDisplay()
  {
    int minutes = (int)(time / 60);
    int seconds = (int)(time % 60);
    timer.text = $"{minutes}mn{seconds:D2}s";  // D2 formats seconds to always show 2 digits
  }
}
