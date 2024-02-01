using TMPro;
using UnityEngine;
using Zenject;

public class GunUpgrade : MonoBehaviour
{
    [Header("Buttons of parameters")]
    [SerializeField] private GameObject _DamageUpgrade;
    [SerializeField] private GameObject FireRateUpgrade;
    [SerializeField] private GameObject SpredUpgrade;
    [SerializeField] private GameObject BulletAmmountUpgrade;
    [SerializeField] private GameObject ReloadingTimeUpgrade;
    [SerializeField] private GameObject Price;


    [Header("Upgrade value of parameters")]
    [SerializeField] private TextMeshProUGUI _damageUpgradeCost;
    [SerializeField] private TextMeshProUGUI FireRateUpgradeCost;
    [SerializeField] private TextMeshProUGUI SpredUpgradeCost;
    [SerializeField] private TextMeshProUGUI BulletAmmountUpgradeCost;
    [SerializeField] private TextMeshProUGUI ReloadingTimeUpgradeCost;


    [Header("Value of parameters")]
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] TextMeshProUGUI _fireRateText;
    [SerializeField] TextMeshProUGUI _accuranceText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] TextMeshProUGUI _reloadingText;

    [Space(10)]
    [SerializeField] private WeaponChooser _weaponChooser;
    [SerializeField] private WeaponSaveData _weaponSaveData;
    private UIMoneyShower _money;

    [Inject]
    private void Construct(UIMoneyShower money)
    {
        _money = money;
    }

    public void Upgrade(string parameterToUpgrade)
    {
        var gunData = _weaponSaveData.ByedWeapon.Find(x => x == _weaponChooser.WeaponsData[_weaponChooser.QueuePosition - 1]);
                
        switch (parameterToUpgrade)
        {
            case "Damage":
                if (_money.Money >= gunData.DamageUpgradeCost)
                {
                    gunData.UpgradeDamage();
                    ChangeValuesOfParameter(ref gunData.DamageUpgradeCost, _damageUpgradeCost, gunData.CostMultiplyIndex,
                        _damageText, ref gunData.Damage);
                }
                break;
        }
    }

    private void ChangeValuesOfParameter(ref int upgradeCost, TextMeshProUGUI textCost, float costMultiplayer, TextMeshProUGUI textValue, ref int parameter)
    {
        _money.ChangeMoneyValue(-upgradeCost);
        upgradeCost = Mathf.FloorToInt(upgradeCost * costMultiplayer);
        textCost.text = upgradeCost.ToString();
        textValue.text = parameter.ToString();
        _weaponChooser.MoneyText.text = _money.Money.ToString();
    }

    public void RemoveUpgradeIcon()
    {
        _DamageUpgrade.gameObject.SetActive(false);
        FireRateUpgrade.gameObject.SetActive(false);
        SpredUpgrade.gameObject.SetActive(false);
        BulletAmmountUpgrade.gameObject.SetActive(false);
        ReloadingTimeUpgrade.gameObject.SetActive(false);
        Price.gameObject.SetActive(true);
    }

    public void ActivateUpgradeIcon()
    {
        _DamageUpgrade.gameObject.SetActive(true);
        FireRateUpgrade.gameObject.SetActive(true);
        SpredUpgrade.gameObject.SetActive(true);
        BulletAmmountUpgrade.gameObject.SetActive(true);
        ReloadingTimeUpgrade.gameObject.SetActive(true);
        Price.gameObject.SetActive(false);


        ChangeAllUpgradesCost();
    }

    public void ChangeAllUpgradesCost()
    {
        if (_weaponChooser.QueuePosition - 1 < _weaponChooser.WeaponsData.Count)
        {
            var gunData = _weaponSaveData.ByedWeapon.Find(x => x == _weaponChooser.WeaponsData[_weaponChooser.QueuePosition - 1]);
            _damageUpgradeCost.text = gunData.DamageUpgradeCost.ToString();
            FireRateUpgradeCost.text = gunData.RateOfFireUpgradeCost.ToString();
            SpredUpgradeCost.text = gunData.SpredUpgradeCost.ToString();
            BulletAmmountUpgradeCost.text = gunData.AmmoUpgradeCost.ToString();
            ReloadingTimeUpgradeCost.text = gunData.ReloadingUpgradeCost.ToString();
        }
    }
}
