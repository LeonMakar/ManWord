using System;
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
                if (_money.AllMoney >= gunData.DamageUpgradeCost && gunData.DamageUpStep < 5)
                {
                    gunData.UpgradeDamage();
                    ChangeValuesOfParameter(ref gunData.DamageUpgradeCost, _damageUpgradeCost, gunData.CostMultiplyIndex,
                        _damageText, ref gunData.Damage);
                    gunData.DamageUpStep++;
                    if (gunData.DamageUpStep >= 5)
                        RemoveSprecificIcon(_DamageUpgrade);
                }
                break;
            case "RateOfFire":
                if (_money.AllMoney >= gunData.RateOfFireUpgradeCost && gunData.RateOfFireUpStep < 5)
                {
                    gunData.UpgradeRateOfFire();
                    ChangeValuesOfParameter(ref gunData.RateOfFireUpgradeCost, FireRateUpgradeCost, gunData.CostMultiplyIndex,
                        _fireRateText, ref gunData.RateOfFire);
                    gunData.RateOfFireUpStep++;
                    if (gunData.RateOfFireUpStep >= 5)
                        RemoveSprecificIcon(FireRateUpgrade);
                }
                break;
            case "Spred":
                if (_money.AllMoney >= gunData.SpredUpgradeCost && gunData.SpredUpStep < 5)
                {
                    gunData.UpgradeGunSpred();
                    ChangeValuesOfParameter(ref gunData.SpredUpgradeCost, SpredUpgradeCost, gunData.CostMultiplyIndex,
                        _accuranceText, ref gunData.GunSpred);
                    gunData.SpredUpStep++;
                    if (gunData.SpredUpStep >= 5)
                        RemoveSprecificIcon(SpredUpgrade);
                }
                break;
            case "Ammo":
                if (_money.AllMoney >= gunData.AmmoUpgradeCost && gunData.AmmoUpStep < 5)
                {
                    gunData.UpgradeBulletAmmount();
                    ChangeValuesOfParameter(ref gunData.AmmoUpgradeCost, BulletAmmountUpgradeCost, gunData.CostMultiplyIndex,
                        _ammoText, ref gunData.BulletAmmount);
                    gunData.AmmoUpStep++;
                    if (gunData.AmmoUpStep >= 5)
                        RemoveSprecificIcon(BulletAmmountUpgrade);
                }
                break;
            case "Reloading":
                if (_money.AllMoney >= gunData.ReloadingUpgradeCost && gunData.ReloadingUpStep < 5)
                {
                    gunData.UpgradeReloadingTime();
                    ChangeValuesOfParameter(ref gunData.ReloadingUpgradeCost, ReloadingTimeUpgradeCost, gunData.CostMultiplyIndex,
                        _reloadingText, ref gunData.ReloadingTime);
                    gunData.ReloadingUpStep++;
                    if (gunData.ReloadingUpStep >= 5)
                        RemoveSprecificIcon(ReloadingTimeUpgrade);

                }
                break;
        }
    }
    private void ChangeValuesOfParameter(ref int upgradeCost, TextMeshProUGUI textCost, float costMultiplayer, TextMeshProUGUI textValue, ref float parameter)
    {
        _money.AllMoney -= upgradeCost;
        upgradeCost = Mathf.FloorToInt(upgradeCost * costMultiplayer);
        textCost.text = upgradeCost.ToString() + "$";
        textValue.text = Math.Round((double)parameter, 2).ToString();
        _weaponChooser.MoneyText.text = _money.AllMoney.ToString();
    }
    private void ChangeValuesOfParameter(ref int upgradeCost, TextMeshProUGUI textCost, float costMultiplayer, TextMeshProUGUI textValue, ref int parameter)
    {
        _money.AllMoney -= upgradeCost;
        upgradeCost = Mathf.FloorToInt(upgradeCost * costMultiplayer);
        textCost.text = upgradeCost.ToString() + "$";
        textValue.text = parameter.ToString();
        _weaponChooser.MoneyText.text = _money.AllMoney.ToString();
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

    public void RemoveSprecificIcon(GameObject upgradeIcon) => upgradeIcon.gameObject.SetActive(false);

    public void ActivateSprecificIcon(GameObject upgradeIcon) => upgradeIcon.gameObject.SetActive(true);


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
            if (gunData != null)
            {
                _damageUpgradeCost.text = gunData.DamageUpgradeCost.ToString() + "$";
                FireRateUpgradeCost.text = Math.Round((double)gunData.RateOfFireUpgradeCost, 2).ToString() + "$";
                SpredUpgradeCost.text = Math.Round((double)gunData.SpredUpgradeCost, 2).ToString() + "$";
                BulletAmmountUpgradeCost.text = gunData.AmmoUpgradeCost.ToString() + "$";
                ReloadingTimeUpgradeCost.text = Math.Round((double)gunData.ReloadingUpgradeCost, 2).ToString() + "$";

                if (gunData.DamageUpStep >= 5)
                    RemoveSprecificIcon(_DamageUpgrade);
                else
                    ActivateSprecificIcon(_DamageUpgrade);

                if (gunData.RateOfFireUpStep >= 5)
                    RemoveSprecificIcon(FireRateUpgrade);
                else
                    ActivateSprecificIcon(FireRateUpgrade);

                if (gunData.SpredUpStep >= 5)
                    RemoveSprecificIcon(SpredUpgrade);
                else
                    ActivateSprecificIcon(SpredUpgrade);

                if (gunData.AmmoUpStep >= 5)
                    RemoveSprecificIcon(BulletAmmountUpgrade);
                else
                    ActivateSprecificIcon(BulletAmmountUpgrade);

                if (gunData.ReloadingUpStep >= 5)
                    RemoveSprecificIcon(ReloadingTimeUpgrade);
                else
                    ActivateSprecificIcon(ReloadingTimeUpgrade);

            }
        }
    }
}
