using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static int selectedLevel;

  public void OnSelectLevel(int level)
  {
    selectedLevel = level;
  }
}