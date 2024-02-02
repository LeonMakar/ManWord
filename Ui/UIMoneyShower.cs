using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class UIMoneyShower : MonoBehaviour
{

    public int Money { get; private set; }

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void Loading()
    {
        Money = YandexGame.savesData.Money;
        ChangeMoneyValue(10000);
    }
    public void ChangeMoneyValue(int money)
    {

        Money += money;
        ChangeText();
    }

    public void ChangeText()
    {
        _moneyText.text = Money.ToString();
    }

    public void AddGold(int gold)
    {

    }

}
