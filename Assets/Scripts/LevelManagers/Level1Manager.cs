using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Level1Manager : GameplayManager
{
  protected override void Start()
  {
    base.Start();
    InitGame();
    // StartCoroutine(Countdown());
  }

  private void InitGame()
  {
    time = 120f;
  }
}
