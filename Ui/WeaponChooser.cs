using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponChooser : MonoBehaviour
{


    //Parameters Fields//
    [Header("Gun Parameters")]
    [SerializeField] GameObject _allGunParameters;
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] TextMeshProUGUI _fireRateText;
    [SerializeField] TextMeshProUGUI _accuranceText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] TextMeshProUGUI _reloadingText;
    [SerializeField] TextMeshProUGUI _costText;
    [SerializeField] TextMeshProUGUI _weaponNameText;

    [Header("Button")]
    [SerializeField] TextMeshProUGUI _buyAndSelectButtoneText;
    [SerializeField] Button _buyButtone;
    [SerializeField] Button _selectButtone;

    private GameObject _activeButton;



    [Space(20)]
    [field: SerializeField] public List<GunData> WeaponsData = new List<GunData>();
    [SerializeField] Transform _weaponStartPosition;
    [SerializeField] Transform _weaponFinishPosition;
    [SerializeField] Transform _weaponFarAwayPosition;
    [SerializeField] Transform _parentObject;
    [SerializeField] Transform _objectToCopieRotation;

    [SerializeField] private float _swiftSpeed;

    [field: SerializeField] public TextMeshProUGUI MoneyText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI GoldText { get; private set; }

    [SerializeField] private GameObject _priceTypeGold;
    [SerializeField] private GameObject _priceTypeMoney;



    public GunData _choosenGun { get; private set; }

    private GameObject _oldWeapon;
    private GameObject _newWeapon;
    private List<GameObject> _weaponsMemmoryList = new List<GameObject>();
    public int QueuePosition { get; private set; }
    private float _time;
    private Gun _gun;
    private WeaponSaveData _weaponSaveData;
    private UIMoneyShower _moneyShower;
    private bool _isSwiping;
    private Action<bool> _deleteGun;
    public static Action Swipe;



    [Inject]
    private void Construct(Gun gun, WeaponSaveData weaponSaveData, UIMoneyShower moneyShower)
    {
        _gun = gun;
        _weaponSaveData = weaponSaveData;
        _moneyShower = moneyShower;
        _deleteGun += DeleteGunFromList;
    }

    #region WeaponeSwipeMooving

    public void ChangeToNextWeapon()
    {
        if (QueuePosition < WeaponsData.Count && !_isSwiping)
        {
            _isSwiping = true;
            _allGunParameters.SetActive(true);
            if (_newWeapon != null)
            {
                if (_oldWeapon != null)
                    _weaponsMemmoryList.Add(_oldWeapon);
                _oldWeapon = _newWeapon;
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponFarAwayPosition, _oldWeapon));
            }
            _newWeapon = Instantiate(WeaponsData[QueuePosition].GunPrefab, _parentObject);
            _newWeapon.transform.position = _weaponStartPosition.position;
            _newWeapon.transform.rotation = _objectToCopieRotation.rotation;
            StartCoroutine(MooveWeapon(_weaponStartPosition, _weaponFinishPosition, _newWeapon));
            if (QueuePosition != 0)
                Swipe.Invoke();
            QueuePosition++;
            ChangeParametersText();
        }
        else if (QueuePosition == WeaponsData.Count && !_isSwiping)
        {
            if (_newWeapon != null)
            {
                _isSwiping = true;

                if (_oldWeapon != null)
                    _weaponsMemmoryList.Add(_oldWeapon);
                _oldWeapon = _newWeapon;
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponFarAwayPosition, _oldWeapon));
                _newWeapon = null;
                QueuePosition++;
            }
            _allGunParameters.SetActive(false);

        }
    }
    public void ChangeToNextWeapon(bool doIDeleteByuedGun)
    {

        if (QueuePosition < WeaponsData.Count && !_isSwiping)
        {
            _isSwiping = true;
            _allGunParameters.SetActive(true);
            if (_newWeapon != null)
            {
                if (_oldWeapon != null)
                    _weaponsMemmoryList.Add(_oldWeapon);
                _oldWeapon = _newWeapon;
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponFarAwayPosition, _oldWeapon));
            }
            _newWeapon = Instantiate(WeaponsData[QueuePosition].GunPrefab, _parentObject);
            _newWeapon.transform.position = _weaponStartPosition.position;
            _newWeapon.transform.rotation = _objectToCopieRotation.rotation;
            StartCoroutine(MooveWeapon(_weaponStartPosition, _weaponFinishPosition, _newWeapon));
            if (QueuePosition != 0)
                Swipe.Invoke();
            QueuePosition++;
            ChangeParametersText();
            _deleteGun.Invoke(doIDeleteByuedGun);
        }
        else if (QueuePosition == WeaponsData.Count && !_isSwiping)
        {
            if (_newWeapon != null)
            {
                _isSwiping = true;

                if (_oldWeapon != null)
                    _weaponsMemmoryList.Add(_oldWeapon);
                _oldWeapon = _newWeapon;
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponFarAwayPosition, _oldWeapon));
                QueuePosition++;

                _deleteGun.Invoke(doIDeleteByuedGun);
            }
            _allGunParameters.SetActive(false);
        }
    }
    public void ChangeToPreviousWeapon()
    {
        if (QueuePosition > 1)
        {

            if (_newWeapon != null && !_isSwiping)
            {
                _isSwiping = true;
                _allGunParameters.SetActive(true);
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponStartPosition, _newWeapon, true));
                if (_oldWeapon != null)
                {
                    StartCoroutine(MooveWeapon(_weaponFarAwayPosition, _weaponFinishPosition, _oldWeapon));
                    _newWeapon = _oldWeapon;
                    if (_weaponsMemmoryList.Count > 0)
                    {
                        _oldWeapon = _weaponsMemmoryList.Last();
                        _weaponsMemmoryList.Remove(_weaponsMemmoryList.Last());
                    }
                    else
                        _oldWeapon = null;
                }
                else
                {
                    StartCoroutine(MooveWeapon(_weaponFarAwayPosition, _weaponFinishPosition, _weaponsMemmoryList.Last()));
                    _weaponsMemmoryList.Remove(_weaponsMemmoryList.Last());
                    _newWeapon = _oldWeapon;

                }
                QueuePosition--;
                ChangeParametersText();
                Swipe.Invoke();
            }
            else if (_newWeapon == null && !_isSwiping)
            {
                _isSwiping = true;
                _allGunParameters.SetActive(true);
                if (_oldWeapon != null)
                {
                    StartCoroutine(MooveWeapon(_weaponFarAwayPosition, _weaponFinishPosition, _oldWeapon));
                    _newWeapon = _oldWeapon;
                    if (_weaponsMemmoryList.Count > 0)
                    {
                        _oldWeapon = _weaponsMemmoryList.Last();
                        _weaponsMemmoryList.Remove(_weaponsMemmoryList.Last());
                    }
                    else
                        _oldWeapon = null;
                    QueuePosition--;
                    ChangeParametersText();
                    Swipe.Invoke();
                }
            }
        }
    }
    private IEnumerator MooveWeapon(Transform startPosition, Transform finishPosition, GameObject weaponToMoove)
    {
        _time = 0;
        _activeButton.gameObject.SetActive(false);
        while (_time < 1.2f)
        {
            yield return null;
            weaponToMoove.transform.position = Vector3.Lerp(startPosition.position, finishPosition.position, _time * _swiftSpeed);
        }
        _isSwiping = false;
        if (QueuePosition <= WeaponsData.Count)
            _activeButton.gameObject.SetActive(true);

    }
    private IEnumerator MooveWeapon(Transform startPosition, Transform finishPosition, GameObject weaponToMoove, bool doDestroy)
    {
        _time = 0;
        _activeButton.gameObject.SetActive(false);
        while (_time < 1.2f)
        {
            yield return null;
            weaponToMoove.transform.position = Vector3.Lerp(startPosition.position, finishPosition.position, _time * _swiftSpeed);
        }
        if (doDestroy)
            Destroy(weaponToMoove.gameObject);
        _isSwiping = false;
        if (QueuePosition <= WeaponsData.Count)
            _activeButton.gameObject.SetActive(true);

    }
    private void ChangeParametersText()
    {
        _damageText.text = WeaponsData[QueuePosition - 1].Damage.ToString();
        _fireRateText.text = Math.Round(WeaponsData[QueuePosition - 1].RateOfFire, 2).ToString();
        _accuranceText.text = Math.Round(WeaponsData[QueuePosition - 1].GunSpred, 2).ToString();
        _ammoText.text = WeaponsData[QueuePosition - 1].BulletAmmount.ToString();
        _reloadingText.text = Math.Round(WeaponsData[QueuePosition - 1].ReloadingTime, 2).ToString() + " sec";
        _costText.text = WeaponsData[QueuePosition - 1].GunCoast.ToString();
        _weaponNameText.text = WeaponsData[QueuePosition - 1].GunName.ToString();
        if (WeaponsData[QueuePosition - 1].PurchaseType == PurchaseType.Money)
        {
            _priceTypeMoney.SetActive(true);
            _priceTypeGold.SetActive(false);
        }
        else
        {
            _priceTypeMoney.SetActive(false);
            _priceTypeGold.SetActive(true);
        }

    }
    #endregion

    #region Initialization  
    public void SetMenuAsByingMenu()
    {
        if (_activeButton != null)
            _activeButton.SetActive(false);
        WeaponsData = _weaponSaveData.NonByedWeapon;
        _activeButton = _buyButtone.gameObject;
        _activeButton.SetActive(true);
        _buyAndSelectButtoneText.text = "Buy";
        MoneyText.text = _moneyShower.AllMoney.ToString();
        GoldText.text = _moneyShower.AllGold.ToString();


    }
    public void SetMenuAsSelectingMenu()
    {
        if (_activeButton != null)
            _activeButton.SetActive(false);
        WeaponsData = _weaponSaveData.ByedWeapon;
        _activeButton = _selectButtone.gameObject;
        _activeButton.SetActive(true);
        _buyAndSelectButtoneText.text = "Select";
        MoneyText.text = _moneyShower.AllMoney.ToString();
        GoldText.text = _moneyShower.AllGold.ToString();

    }
    #endregion

    public void DeleteMenu()
    {

        foreach (var item in _weaponsMemmoryList)
            Destroy(item.gameObject);

        _weaponsMemmoryList.Clear();
        if (_newWeapon != null)
            Destroy(_newWeapon.gameObject);
        if (_oldWeapon != null)
            Destroy(_oldWeapon.gameObject);
        _newWeapon = null;
        _oldWeapon = null;
        QueuePosition = 0;
    }
    public void SelectWeapone()
    {
        _gun.EqipeNewGun(WeaponsData[QueuePosition - 1]);
        _weaponSaveData.SetNewDefaultGun(WeaponsData[QueuePosition - 1]);
    }
    public void BuyWeapon()
    {
        if (WeaponsData[QueuePosition - 1].PurchaseType == PurchaseType.Money)
        {
            if (_moneyShower.AllMoney >= WeaponsData[QueuePosition - 1].GunCoast)
            {
                _weaponSaveData.ByingWeapon(WeaponsData[QueuePosition - 1], PurchaseType.Money);
                QueuePosition--;
                ChangeToNextWeapon(true);
                MoneyText.text = _moneyShower.AllMoney.ToString();
            }
        }
        else
        {
            if (_moneyShower.AllGold >= WeaponsData[QueuePosition - 1].GunCoast)
            {
                _weaponSaveData.ByingWeapon(WeaponsData[QueuePosition - 1], PurchaseType.Gold);
                QueuePosition--;
                ChangeToNextWeapon(true);
                GoldText.text = _moneyShower.AllGold.ToString();
            }
        }

    }
    private void DeleteGunFromList(bool boolian)
    {
        StartCoroutine(WaitSwipe());
    }

    private IEnumerator WaitSwipe()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(_oldWeapon);
        if (_weaponsMemmoryList.Count > 0)
        {
            _oldWeapon = _weaponsMemmoryList.Last();
            _weaponsMemmoryList.Remove(_weaponsMemmoryList.Last());
        }
        else
            _oldWeapon = null;
    }


    private void Update()
    {
        if (_time < 1.2f)
            _time += Time.deltaTime;
    }

}
