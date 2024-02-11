using TMPro;
using UnityEngine;

public class GameIsOverState : BaseState<GameStateMachine.GameStates>
{
    private Canvas _gameOverCanvas;
    private SaveAndLoadProcess _saveAndLoad;

    private UIMoneyShower _uiMoney;
    private GameOverADS _ads;

    public GameIsOverState(GameStateMachine.GameStates key, Canvas gameOverCanvas, SaveAndLoadProcess saveAndLoad, UIMoneyShower uiMoney, GameOverADS gameOverADS) : base(key)
    {
        _gameOverCanvas = gameOverCanvas;
        _saveAndLoad = saveAndLoad;

        _uiMoney = uiMoney;
        _ads = gameOverADS;
    }

    public override void EnterToState()
    {
        _gameOverCanvas.enabled = true;
        if (_uiMoney.Gold == 0)
            _ads.GoldADS.SetActive(false);
        if (_uiMoney.Money == 0)
            _ads.MoneyADS.SetActive(false);

        _uiMoney.AddIncomeOnGameOver();
        _uiMoney.EarnMoneyText.text = _uiMoney.Money.ToString();
        _uiMoney.EarnGoldText.text = _uiMoney.Gold.ToString();
    }

    public override void ExitFromState()
    {
        _gameOverCanvas.enabled = false;
    }

    public override void UpdateState()
    {
    }
}
