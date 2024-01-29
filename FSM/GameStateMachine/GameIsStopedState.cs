using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIsStopedState : BaseState<GameStateMachine.GameStates>
{

    private Canvas _gameMainCanvas;
    public GameIsStopedState(GameStateMachine.GameStates key, Canvas gameMainCanvas) : base(key)
    {
        _gameMainCanvas = gameMainCanvas;
    }

    public override void EnterToState()
    {

    }

    public override void ExitFromState()
    {
        _gameMainCanvas.enabled = false;
    }

    public override void UpdateState()
    {
    }
}
