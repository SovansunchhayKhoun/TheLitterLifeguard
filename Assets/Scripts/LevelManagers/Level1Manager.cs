using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Level1Manager : GameplayManager
{
  public static int NumTrash = 16;
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
