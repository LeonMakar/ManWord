using TMPro;
using UnityEngine;

public class GameIsOverState : BaseState<GameStateMachine.GameStates>
{
    private Canvas _gameOverCanvas;
    private SaveAndLoadProcess _saveAndLoad;

    private UIMoneyShower _uiMoney;

    public GameIsOverState(GameStateMachine.GameStates key, Canvas gameOverCanvas, SaveAndLoadProcess saveAndLoad, UIMoneyShower uiMoney) : base(key)
    {
        _gameOverCanvas = gameOverCanvas;
        _saveAndLoad = saveAndLoad;

        _uiMoney = uiMoney;
    }

    public override void EnterToState()
    {
        _gameOverCanvas.enabled = true;
        _uiMoney.AddIncomeOnGameOver();
        _uiMoney.EarnMoneyText.text = _uiMoney.Money.ToString();
        _uiMoney.EarnGoldText.text = _uiMoney.Gold.ToString();
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

