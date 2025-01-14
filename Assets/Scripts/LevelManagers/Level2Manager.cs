public class Level2Manager : GameplayManager
{
  public static int NumTrash = 20;
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
