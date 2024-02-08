using System;
using TMPro;
using UnityEngine;
using YG;

public class UIMoneyShower : MonoBehaviour
{

    public int Money { get; private set; }
    public int AllMoney;
    public int Gold { get; private set; }
    public int AllGold;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private GoldChanger _goldChanger;

    [field: SerializeField] public TextMeshProUGUI EarnMoneyText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI EarnGoldText { get; private set; }
    public void Loading()
    {
        AllMoney = YandexGame.savesData.Money;
        AllGold = YandexGame.savesData.Gold;
        YandexGame.RewardVideoEvent += Reward;
        YandexGame.PurchaseSuccessEvent += AddGoldForYAN;

    }

    private void AddGoldForYAN(string obj)
    {
        AllGold += 500;
        _goldChanger.InitGOldAndMoneyCount();
    }
    public void ResetGoldAndMoneyINRestart()
    {
        Gold = 0;
        Money = 0;
    }
    public void Reward(int rewardId)
    {
        switch (rewardId)
        {
            case 0:
                AllMoney += Money;
                EarnMoneyText.text = (Money + Money).ToString();
                break;
            case 1:
                AllGold += Gold;
                EarnGoldText.text = (Gold + Gold).ToString();
                break;
        }
    }

    public void AddIncomeOnGameOver()
    {
        AllMoney += Money;
        AllGold += Gold;
    }
    public void ChangeMoneyValue(int money)
    {
        Money += money;
        ChangeMoneyText();
    }

    public void ChangeMoneyText()
    {
        _moneyText.text = Money.ToString();
    }
    public void ChangeGoldText()
    {
        _goldText.text = Gold.ToString();
    }

    public void AddGold(int gold)
    {
        Gold += gold;
        ChangeGoldText();
    }

}
