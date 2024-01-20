public class StartGameSignal
{

    public bool GameIsStarted { get; private set; } = false;
    public StartGameSignal(bool boolian)
    {
        GameIsStarted = boolian;
    }
}
