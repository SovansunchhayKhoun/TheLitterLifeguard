public class Level2Manager : LevelManager
{
  protected override void Start()
  {
    base.Start();
    InitGame();
  }

  private void InitGame()
  {
    time = 90.0f;
  }
}