public class GameOverSignal
{
    public bool GameIsOver { get; private set; } = false;

    public GameOverSignal(bool gameIsOver)
    {
        GameIsOver = gameIsOver;
    }
}
