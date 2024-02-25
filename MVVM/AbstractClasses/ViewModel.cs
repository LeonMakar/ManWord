using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class ViewModel
{
    protected GunData _model;
    protected Dictionary<string, GunData> _models = new Dictionary<string, GunData>();
    private UIMoneyShower _money;
    public Action<int, int> OnWalletChange;
    public Action<bool> OnToBuyingMenuChenged;

    private bool _isBuyMenu;


    private const int MAX_UPGRADE_STEPS = 5;

    public ReactiveProperty<int> DamageView = new ReactiveProperty<int>();
    public ReactiveProperty<float> RateOfFireView = new ReactiveProperty<float>();
    public ReactiveProperty<float> GunSpredView = new ReactiveProperty<float>();
    public ReactiveProperty<int> BulletAmmountView = new ReactiveProperty<int>();
    public ReactiveProperty<float> ReloadingTimeView = new ReactiveProperty<float>();

    public ReactiveProperty<int> DamageUpgradeCostView = new ReactiveProperty<int>();
    public ReactiveProperty<int> RateOfFireUpgradeCostView = new ReactiveProperty<int>();
    public ReactiveProperty<int> GunSpredUpgradeCostView = new ReactiveProperty<int>();
    public ReactiveProperty<int> BulletAmmountUpgradeCostView = new ReactiveProperty<int>();
    public ReactiveProperty<int> ReloadingTimeUpgradeCostView = new ReactiveProperty<int>();



    public ReactiveProperty<bool> DamageUpgradeButtonEnable = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> RateOfFireUpgradeButtoneEnable = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> GunSpredUpgradeButtoneEnable = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> BulletAmmountUpgradeButtoneEnable = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> ReloadingTimeUpgradeButtoneEnable = new ReactiveProperty<bool>();

    public void Initialize(List<GunData> modelsList, UIMoneyShower money)
    {
        _money = money;
        foreach (var model in modelsList)
        {
            _models.Add(model.GunName, model);
        }
    }
    private void SubscribeToNewModel()
    {
        _model.Damage.OnChange += OnModelDamageChange;
        _model.RateOfFire.OnChange += OnModelRateOfFIreChange;
        _model.GunSpred.OnChange += OnModelGunSpredChange;
        _model.BulletAmmount.OnChange += OnModelBulletAmmountChange;
        _model.ReloadingTime.OnChange += OnModelReloadingTimeChange;

        _model.Damage.Invoke();
        _model.RateOfFire.Invoke();
        _model.GunSpred.Invoke();
        _model.BulletAmmount.Invoke();
        _model.ReloadingTime.Invoke();

        DamageUpgradeCostView.Value = _model.DamageUpgradeCost;
        RateOfFireUpgradeCostView.Value = _model.RateOfFireUpgradeCost;
        GunSpredUpgradeCostView.Value = _model.SpredUpgradeCost;
        BulletAmmountUpgradeCostView.Value = _model.SpredUpgradeCost;
        ReloadingTimeUpgradeCostView.Value = _model.ReloadingUpgradeCost;
    }

    private void OnModelRateOfFIreChange(float value) => RateOfFireView.Value = value;
    private void OnModelGunSpredChange(float value) => GunSpredView.Value = value;
    private void OnModelBulletAmmountChange(int value) => BulletAmmountView.Value = value;
    private void OnModelReloadingTimeChange(float value) => ReloadingTimeView.Value = value;
    private void OnModelDamageChange(int value) => DamageView.Value = value;



    public void OnIncreaseDamageButtoneClicked()
    {
        if (_money.AllMoney >= _model.DamageUpgradeCost)
        {
            IncreasePropertyValue(_model.Damage, _model.DamageUpgradeValue,
                ref _model.DamageUpgradeCost, _model.CostMultiplyIndex, true);
            CheckUpgradeStep(ref _model.DamageUpStep, ref DamageUpgradeButtonEnable);
            DamageUpgradeCostView.Value = _model.DamageUpgradeCost;
        }
    }
    public void OnIncreaseRateOfFireButtoneClicked()
    {
        if (_money.AllMoney >= _model.RateOfFireUpgradeCost)
        {
            IncreasePropertyValue(_model.RateOfFire, _model.RateOfFireUpgradeValue,
                ref _model.RateOfFireUpgradeCost, _model.CostMultiplyIndex, false);
            CheckUpgradeStep(ref _model.RateOfFireUpStep, ref RateOfFireUpgradeButtoneEnable);
            RateOfFireUpgradeCostView.Value = _model.RateOfFireUpgradeCost;
        }
    }
    public void OnIncreaseGunSpredButtoneClicked()
    {
        if (_money.AllMoney >= _model.SpredUpgradeCost)
        {
            IncreasePropertyValue(_model.GunSpred, _model.SpredUpgradeValue, ref _model.SpredUpgradeCost,
                _model.CostMultiplyIndex, false);
            CheckUpgradeStep(ref _model.SpredUpStep, ref GunSpredUpgradeButtoneEnable);
            GunSpredUpgradeCostView.Value = _model.SpredUpgradeCost;
        }
    }
    public void OnIncreaseReloadingTimeButtoneClicked()
    {
        if (_money.AllMoney >= _model.ReloadingUpgradeCost)
        {
            IncreasePropertyValue(_model.ReloadingTime, _model.ReloadingTImeUpgradeValue,
                ref _model.ReloadingUpgradeCost, _model.CostMultiplyIndex, false);
            CheckUpgradeStep(ref _model.ReloadingUpStep, ref ReloadingTimeUpgradeButtoneEnable);
            ReloadingTimeUpgradeCostView.Value = _model.ReloadingUpgradeCost;
        }
    }
    public void OnIncreaseBulletAmmountButtoneClicked()
    {
        if (_money.AllMoney >= _model.AmmoUpgradeCost)
        {
            IncreasePropertyValue(_model.BulletAmmount, _model.BulletAmmountUpgradeValue,
                ref _model.AmmoUpgradeCost, _model.CostMultiplyIndex, true);
            CheckUpgradeStep(ref _model.AmmoUpStep, ref BulletAmmountUpgradeButtoneEnable);
            BulletAmmountUpgradeCostView.Value = _model.AmmoUpgradeCost;
        }
    }
    private void CheckUpgradeStep(ref int upgradeStep, ref ReactiveProperty<bool> property)
    {
        upgradeStep++;
        if (upgradeStep == MAX_UPGRADE_STEPS)
            property.Value = false;
    }
    private void CheckUpgradeStep(ref float upgradeStep, ref ReactiveProperty<bool> property)
    {
        upgradeStep++;
        if (upgradeStep == MAX_UPGRADE_STEPS)
            property.Value = false;
    }

    public void OnSwipeButtoneClicked(GunData model)
    {
        if (_model != null)
            Dispose();

        _model = _models[model.GunName];
        SubscribeToNewModel();
        if (!_isBuyMenu)
            CheckUpgradeButtonsAvalibility();
    }

    private void CheckUpgradeButtonsAvalibility()
    {
        if (_model.DamageUpStep < MAX_UPGRADE_STEPS)
            DamageUpgradeButtonEnable.Value = true;
        if (_model.RateOfFireUpStep < MAX_UPGRADE_STEPS)
            RateOfFireUpgradeButtoneEnable.Value = true;
        if (_model.SpredUpStep < MAX_UPGRADE_STEPS)
            GunSpredUpgradeButtoneEnable.Value = true;
        if (_model.AmmoUpStep < MAX_UPGRADE_STEPS)
            BulletAmmountUpgradeButtoneEnable.Value = true;
        if (_model.ReloadingUpStep < MAX_UPGRADE_STEPS)
            ReloadingTimeUpgradeButtoneEnable.Value = true;
    }


    private void IncreasePropertyValue(ReactiveProperty<int> property, int increaseValue, ref int upgradeCost, float costChangeIndex,
        bool isNeedToIncreaseValue)
    {
        if (isNeedToIncreaseValue)
            property.Value += increaseValue;
        else
            property.Value -= increaseValue;

            _money.AllMoney -= upgradeCost;
        OnWalletChange.Invoke(_money.AllMoney, _money.AllGold);
        

        upgradeCost = Mathf.FloorToInt(upgradeCost * costChangeIndex);
    }
    private void IncreasePropertyValue(ReactiveProperty<float> property, float increaseValue, ref int upgradeCost, float costChangeIndex,
        bool isNeedToIncreaseValue)
    {
        if (isNeedToIncreaseValue)
            property.Value = (float)Math.Round(property.Value + increaseValue, 2);
        else
            property.Value = (float)Math.Round(property.Value - increaseValue, 2);
       
        _money.AllMoney -= upgradeCost;
        OnWalletChange.Invoke(_money.AllMoney, _money.AllGold);


        upgradeCost = Mathf.FloorToInt(upgradeCost * costChangeIndex);
    }

    public virtual void Dispose()
    {
        _model.Damage.OnChange -= OnModelDamageChange;
        _model.RateOfFire.OnChange -= OnModelRateOfFIreChange;
        _model.GunSpred.OnChange -= OnModelGunSpredChange;
        _model.BulletAmmount.OnChange -= OnModelBulletAmmountChange;
        _model.ReloadingTime.OnChange -= OnModelReloadingTimeChange;
    }



    public void OnBuyModeSelected()
    {
        OnWalletChange.Invoke(_money.AllMoney, _money.AllGold);
        _isBuyMenu = true;
        OnToBuyingMenuChenged.Invoke(true);
        DamageUpgradeButtonEnable.Value = false;
        RateOfFireUpgradeButtoneEnable.Value = false;
        GunSpredUpgradeButtoneEnable.Value = false;
        BulletAmmountUpgradeButtoneEnable.Value = false;
        ReloadingTimeUpgradeButtoneEnable.Value = false;

    }

    public void OnSelectWeaponeModeSelected()
    {
        _isBuyMenu = false;
        OnWalletChange.Invoke(_money.AllMoney, _money.AllGold);
        OnToBuyingMenuChenged.Invoke(false);
        DamageUpgradeButtonEnable.Value = true;
        RateOfFireUpgradeButtoneEnable.Value = true;
        GunSpredUpgradeButtoneEnable.Value = true;
        BulletAmmountUpgradeButtoneEnable.Value = true;
        ReloadingTimeUpgradeButtoneEnable.Value = true;

    }
}
