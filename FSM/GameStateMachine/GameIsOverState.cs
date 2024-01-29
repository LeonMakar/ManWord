using UnityEngine;

public class GameIsOverState : BaseState<GameStateMachine.GameStates>
{
    private Canvas _gameOverCanvas;
    private SaveAndLoadProcess _saveAndLoad;

    public GameIsOverState(GameStateMachine.GameStates key, Canvas gameOverCanvas, SaveAndLoadProcess saveAndLoad) : base(key)
    {
        _gameOverCanvas = gameOverCanvas;
        _saveAndLoad = saveAndLoad;
    }

    public override void EnterToState()
    {
        _gameOverCanvas.enabled = true;
        _saveAndLoad.SaveGameData();
    }

    public override void ExitFromState()
    {
        _gameOverCanvas.enabled = false;
    }

    public override void UpdateState()
    {
    }
}

