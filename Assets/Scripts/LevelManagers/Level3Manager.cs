public class Level3Manager : GameplayManager
{
  public static int NumTrash = 24;
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
