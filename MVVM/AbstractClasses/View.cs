using UnityEngine;

public abstract class View : MonoBehaviour
{
    protected ViewModel _viewModel;


    public virtual void Initialize(ViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.DamageView.OnChange += DisplayDamageChange;
        _viewModel.RateOfFireView.OnChange += DisplayRateOfFireChange;
        _viewModel.ReloadingTimeView.OnChange += DisplayReloadingTimeChange;
        _viewModel.GunSpredView.OnChange += DisplayGunSpredChange;
        _viewModel.BulletAmmountView.OnChange += DisplayBulletAmmountChange;

        _viewModel.DamageUpgradeCostView.OnChange += DisplayDamageUpgradeChange;
        _viewModel.RateOfFireUpgradeCostView.OnChange += DisplayRateOfFireUpgradeChange;
        _viewModel.ReloadingTimeUpgradeCostView.OnChange += DisplayReloadingTimeUpgradeChange;
        _viewModel.GunSpredUpgradeCostView.OnChange += DisplayGunSpredUpgradeChange;
        _viewModel.BulletAmmountUpgradeCostView.OnChange += DisplayBulletAmmountUpgradeChange;



        _viewModel.DamageUpgradeButtonEnable.OnChange += OnDamageButtonEnable;
        _viewModel.RateOfFireUpgradeButtoneEnable.OnChange += OnRateOfFireButtonEnable;
        _viewModel.ReloadingTimeUpgradeButtoneEnable.OnChange += OnReloadingTimeButtonEnable;
        _viewModel.GunSpredUpgradeButtoneEnable.OnChange += OnGunSpredButtonEnable;
        _viewModel.BulletAmmountUpgradeButtoneEnable.OnChange += OnBulletAmmountButtonEnable;

        _viewModel.OnWalletChange += OnMoneyOrGoldChanged;
        _viewModel.OnToBuyingMenuChenged += OnToBuyingMenuChanged;
    }

    protected abstract void OnToBuyingMenuChanged(bool boolian);


    protected abstract void DisplayDamageChange(int value);
    protected abstract void DisplayRateOfFireChange(float value);
    protected abstract void DisplayReloadingTimeChange(float value);
    protected abstract void DisplayGunSpredChange(float value);
    protected abstract void DisplayBulletAmmountChange(int value);

    protected abstract void DisplayDamageUpgradeChange(int value);
    protected abstract void DisplayRateOfFireUpgradeChange(int value);
    protected abstract void DisplayReloadingTimeUpgradeChange(int value);
    protected abstract void DisplayGunSpredUpgradeChange(int value);
    protected abstract void DisplayBulletAmmountUpgradeChange(int value);


    protected abstract void OnDamageButtonEnable(bool value);
    protected abstract void OnRateOfFireButtonEnable(bool value);
    protected abstract void OnReloadingTimeButtonEnable(bool value);
    protected abstract void OnGunSpredButtonEnable(bool value);
    protected abstract void OnBulletAmmountButtonEnable(bool value);

    protected abstract void OnMoneyOrGoldChanged(int money, int gold);
    protected virtual void Dispose()
    {
        _viewModel.DamageView.OnChange -= DisplayDamageChange;
        _viewModel.RateOfFireView.OnChange -= DisplayRateOfFireChange;
        _viewModel.ReloadingTimeView.OnChange -= DisplayReloadingTimeChange;
        _viewModel.GunSpredView.OnChange -= DisplayGunSpredChange;
        _viewModel.BulletAmmountView.OnChange -= DisplayBulletAmmountChange;

        _viewModel.DamageUpgradeCostView.OnChange -= DisplayDamageUpgradeChange;
        _viewModel.RateOfFireUpgradeCostView.OnChange -= DisplayRateOfFireUpgradeChange;
        _viewModel.ReloadingTimeUpgradeCostView.OnChange -= DisplayReloadingTimeUpgradeChange;
        _viewModel.GunSpredUpgradeCostView.OnChange -= DisplayGunSpredUpgradeChange;
        _viewModel.BulletAmmountUpgradeCostView.OnChange -= DisplayBulletAmmountUpgradeChange;

        _viewModel.DamageUpgradeButtonEnable.OnChange -= OnDamageButtonEnable;
        _viewModel.RateOfFireUpgradeButtoneEnable.OnChange -= OnRateOfFireButtonEnable;
        _viewModel.ReloadingTimeUpgradeButtoneEnable.OnChange -= OnReloadingTimeButtonEnable;
        _viewModel.GunSpredUpgradeButtoneEnable.OnChange -= OnGunSpredButtonEnable;
        _viewModel.BulletAmmountUpgradeButtoneEnable.OnChange -= OnBulletAmmountButtonEnable;

        _viewModel.OnWalletChange -= OnMoneyOrGoldChanged;

    }

    protected void OnDestroy()
    {
        Dispose();
    }

}
