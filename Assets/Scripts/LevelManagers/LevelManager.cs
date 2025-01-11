using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelManager : MonoBehaviour
{
  public TextMeshProUGUI ScoreText;
  public TextMeshProUGUI ResultText;
  public GameObject gameOverPanel;

  public void ToggleGameOver()
  {
    ScoreText.text = UIManager.points.ToString();
    gameOverPanel.SetActive(true);
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