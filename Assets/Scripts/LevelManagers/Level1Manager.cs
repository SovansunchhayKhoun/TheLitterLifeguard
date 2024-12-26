using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Level1Manager : MonoBehaviour
{
  public Text timer;
  private int time = 120;

  public void Start()
  {
    UpdateTimerDisplay();
    StartCoroutine(Countdown());
  }

  private IEnumerator Countdown()
  {
    while (time > 0)
    {
      yield return new WaitForSeconds(1f);
      time -= 1;
      UpdateTimerDisplay();
    }

    // Game Over when timer hits 0
    Debug.Log("Game over");
  }

  private void UpdateTimerDisplay()
  {
    int minutes = time / 60;
    int seconds = time % 60;
    timer.text = $"{minutes}mn{seconds:D2}s";  // D2 formats seconds to always show 2 digits
  }
}
