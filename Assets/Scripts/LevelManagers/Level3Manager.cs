public class Level3Manager : GameplayManager
{
  protected override void Start()
  {
    base.Start();
    InitGame();
  }

  private void InitGame()
  {
    time = 60.0f;
  }
}