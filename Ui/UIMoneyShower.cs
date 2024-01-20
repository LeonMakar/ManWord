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

    private void Awake()
    {
        YandexGame.GetDataEvent += Initialize;
    }
    private void Initialize()
    {
        Debug.Log("Вызвов сработал");
        Money = YandexGame.savesData.Money;
        ChangeText();
    }
    public void AddMoney(int money)
    {
        if (money > 0)
        {
            Money += money;
            ChangeText();
        }
    }

    private void ChangeText()
    {
        _moneyText.text = Money.ToString();
    }

    public void AddGold(int gold)
    {

    }

}
