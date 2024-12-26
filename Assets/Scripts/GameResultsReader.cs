using System.IO;
using UnityEngine;
using TMPro;

public class GameResultsReader : MonoBehaviour
{
  [SerializeField] private TMP_Text resultsText;  // Reference to UI text element
  private string filePath;


  void Start()
  {
    filePath = Directory.GetParent(Application.dataPath).FullName + "/GameResults.txt";
    DisplayPoints();
  }

  private void DisplayPoints()
  {
    if (File.Exists(filePath))
    {
      string[] lines = File.ReadAllLines(filePath);
      foreach (string line in lines)
      {
        if (line.StartsWith("Points:"))
        {
          string points = line.Replace("Points: ", "");  // Extract points value
          resultsText.text = points;
          Debug.Log("Points Displayed: " + points);
          return;
        }
      }
    }
    else
    {
      Debug.LogWarning("GameResults.txt not found.");
      resultsText.text = "Points: 0";
    }
  }
}
