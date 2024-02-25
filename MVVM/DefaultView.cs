using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultView : View
{
    [Header("Gun Parameters")]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _rateOfFireText;
    [SerializeField] private TextMeshProUGUI _bulletAmmountText;
    [SerializeField] private TextMeshProUGUI _gunSpredText;
    [SerializeField] private TextMeshProUGUI _reloadingTimeText;

    [Header("Gun Upgrade Parameters")]
    [SerializeField] private TextMeshProUGUI _damageUpgradeText;
    [SerializeField] private TextMeshProUGUI _rateOfFireUpgradeText;
    [SerializeField] private TextMeshProUGUI _bulletAmmountUpgradeText;
    [SerializeField] private TextMeshProUGUI _gunSpredUpgradeText;
    [SerializeField] private TextMeshProUGUI _reloadingTimeUpgradeText;

    [Header("Wallet Parameters")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _goldText;





    [Header("Parameters Upgrade Buttons")]
    [SerializeField] private Button _addDamageButtone;
    [SerializeField] private Button _addRateOfFireButtone;
    [SerializeField] private Button _addBulletAmmountButtone;
    [SerializeField] private Button _addGunSpredButtone;
    [SerializeField] private Button _addReloadingTimeButtone;


    [Header("Wallet Buttons")]
    [SerializeField] private Button _buyButtone;
    [SerializeField] private Button _selectButtone;


    [Header("Upgrade Icons")]
    [SerializeField] private GameObject _activateDamageButtone;
    [SerializeField] private GameObject _activateRateOfFireButtone;
    [SerializeField] private GameObject _activateBulletAmmountButtone;
    [SerializeField] private GameObject _activateGunSpredButtone;
    [SerializeField] private GameObject _activateReloadingTimeButtone;




    [SerializeField] private GameObject Price;

    public override void Initialize(ViewModel viewModel)
    {
        base.Initialize(viewModel);

        _addDamageButtone.onClick.AddListener(_viewModel.OnIncreaseDamageButtoneClicked);
        _addRateOfFireButtone.onClick.AddListener(_viewModel.OnIncreaseRateOfFireButtoneClicked);
        _addBulletAmmountButtone.onClick.AddListener(_viewModel.OnIncreaseBulletAmmountButtoneClicked);
        _addGunSpredButtone.onClick.AddListener(_viewModel.OnIncreaseGunSpredButtoneClicked);
        _addReloadingTimeButtone.onClick.AddListener(_viewModel.OnIncreaseReloadingTimeButtoneClicked);
        _buyButtone.onClick.AddListener(_viewModel.OnBuyModeSelected);
        _selectButtone.onClick.AddListener(_viewModel.OnSelectWeaponeModeSelected);

        //_damageText.text = _viewModel.DamageView.Value.ToString();
        //_rateOfFireText.text = _viewModel.RateOfFireView.Value.ToString();
        //_bulletAmmountText.text = _viewModel.BulletAmmountView.Value.ToString();
        //_gunSpredText.text = _viewModel.GunSpredView.Value.ToString();
        //_reloadingTimeText.text = _viewModel.ReloadingTimeView.Value.ToString();
    }

    protected override void DisplayBulletAmmountChange(int value)
    {
        _bulletAmmountText.text = value.ToString();
    }

    protected override void DisplayBulletAmmountUpgradeChange(int value)
    {
        _bulletAmmountUpgradeText.text = value.ToString()+"$";
    }

    protected override void DisplayDamageChange(int value)
    {
        _damageText.text = value.ToString();
    }

    protected override void DisplayDamageUpgradeChange(int value)
    {
        _damageUpgradeText.text = value.ToString() + "$";
    }

    protected override void DisplayGunSpredChange(float value)
    {
        _gunSpredText.text = value.ToString();
    }

    protected override void DisplayGunSpredUpgradeChange(int value)
    {
       _gunSpredUpgradeText.text = value.ToString() + "$";
    }

    protected override void DisplayRateOfFireChange(float value)
    {
        _rateOfFireText.text = value.ToString();
    }

    protected override void DisplayRateOfFireUpgradeChange(int value)
    {
        _rateOfFireUpgradeText.text = value.ToString() + "$";
    }

    protected override void DisplayReloadingTimeChange(float value)
    {
        _reloadingTimeText.text = value.ToString();
    }

    protected override void DisplayReloadingTimeUpgradeChange(int value)
    {
        _reloadingTimeUpgradeText.text = value.ToString() + "$";
    }

    protected override void OnBulletAmmountButtonEnable(bool value)
    {
        _activateBulletAmmountButtone.SetActive(value);
    }

    protected override void OnDamageButtonEnable(bool value)
    {
        _activateDamageButtone.SetActive(value);
    }

    protected override void OnGunSpredButtonEnable(bool value)
    {
        _activateGunSpredButtone.SetActive(value);
    }

    protected override void OnMoneyOrGoldChanged(int money, int gold)
    {
        _moneyText.text = money.ToString();
        _goldText.text = gold.ToString();
    }

    protected override void OnRateOfFireButtonEnable(bool value)
    {
        _activateRateOfFireButtone.SetActive(value);
    }

    protected override void OnReloadingTimeButtonEnable(bool value)
    {
        _activateReloadingTimeButtone.SetActive(value);
    }

    protected override void OnToBuyingMenuChanged(bool boolian)
    {
       Price.gameObject.SetActive(boolian);
    }
}
