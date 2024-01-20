using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Zenject;

public class WeaponChooser : MonoBehaviour
{


    //Parameters Fields//
    [Header("Gun Parameters")]
    [SerializeField] TextMeshProUGUI _damageText;
    [SerializeField] TextMeshProUGUI _fireRateText;
    [SerializeField] TextMeshProUGUI _accuranceText;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] TextMeshProUGUI _reloadingText;
    [SerializeField] TextMeshProUGUI _costText;
    [SerializeField] TextMeshProUGUI _weaponNameText;

    [Header("Button")]
    [SerializeField] TextMeshProUGUI _buyAndSelectButtoneText;
    [SerializeField] Button _buyAndSelectButtone;


    [Space(20)]
    [SerializeField] List<GunData> _weaponsData = new List<GunData>();
    [SerializeField] Transform _weaponStartPosition;
    [SerializeField] Transform _weaponFinishPosition;
    [SerializeField] Transform _weaponFarAwayPosition;
    [SerializeField] Transform _parentObject;
    [SerializeField] Transform _objectToCopieRotation;

    [SerializeField] private float _swiftSpeed;

    [SerializeField] private TextMeshProUGUI _moneyText;


    public GunData _choosenGun { get; private set; }

    private GameObject _oldWeapon;
    private GameObject _newWeapon;
    private List<GameObject> _weaponsMemmoryList = new List<GameObject>();
    private int _queuePosition;
    private float _time;
    private Gun _gun;
    private WeaponSaveData _weaponSaveData;
    private UIMoneyShower _moneyShower;
    private bool _isSwiping;
    public static Action Swipe;



    [Inject]
    private void Construct(Gun gun, WeaponSaveData weaponSaveData, UIMoneyShower moneyShower)
    {
        _gun = gun;
        _weaponSaveData = weaponSaveData;
        _moneyShower = moneyShower;
    }

    #region WeaponeSwipeMooving

    public void ChangeToNextWeapon()
    {

        if (_queuePosition < _weaponsData.Count && !_isSwiping)
        {
            _isSwiping = true;
            if (_newWeapon != null)
            {
                if (_oldWeapon != null)
                    _weaponsMemmoryList.Add(_oldWeapon);
                _oldWeapon = _newWeapon;
                StartCoroutine(MooveWeapon(_weaponFinishPosition, _weaponFarAwayPosition, _oldWeapon));
            }
            _newWeapon = Instantiate(_weaponsData[_queuePosition].GunPrefab, _parentObject);
            _newWeapon.transform.position = _weaponStartPosition.position;
            _newWeapon.transform.rotation = _objectToCopieRotation.rotation;
            StartCoroutine(MooveWeapon(_weaponStartPosition, _weaponFinishPosition, _newWeapon));
            if (_queuePosition != 0)
                Swipe.Invoke();
            _queuePosition++;
            ChangeParametersText();
        }
    }
    public void ChangeToPreviousWeapon()
    {
        if (_queuePosition > 1)
            if (_newWeapon != null && !_isSwiping)
            {
                _isSwiping = true;
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
                _queuePosition--;
                ChangeParametersText();
                Swipe.Invoke();
            }
    }
    private IEnumerator MooveWeapon(Transform startPosition, Transform finishPosition, GameObject weaponToMoove)
    {
        _time = 0;
        while (_time < 1.2f)
        {
            yield return null;
            weaponToMoove.transform.position = Vector3.Lerp(startPosition.position, finishPosition.position, _time * _swiftSpeed);
        }
        _isSwiping = false;
    }
    private IEnumerator MooveWeapon(Transform startPosition, Transform finishPosition, GameObject weaponToMoove, bool boolian)
    {
        _time = 0;
        while (_time < 1.2f)
        {
            yield return null;
            weaponToMoove.transform.position = Vector3.Lerp(startPosition.position, finishPosition.position, _time * _swiftSpeed);
        }
        if (boolian)
            Destroy(weaponToMoove.gameObject);
        _isSwiping = false;
    }
    private void ChangeParametersText()
    {
        _damageText.text = _weaponsData[_queuePosition - 1].Damage.ToString();
        _fireRateText.text = _weaponsData[_queuePosition - 1].RateOfFire.ToString();
        _accuranceText.text = _weaponsData[_queuePosition - 1].GunSpred.ToString();
        _ammoText.text = _weaponsData[_queuePosition - 1].BulletAmmount.ToString();
        _reloadingText.text = _weaponsData[_queuePosition - 1].ReloadingTime.ToString() + " sec";
        _costText.text = _weaponsData[_queuePosition - 1].GunCoast.ToString();
        _weaponNameText.text = _weaponsData[_queuePosition - 1].GunName.ToString();
    }
    #endregion

    #region Initialization 
    public void SetMenuAsByingMenu()
    {
        _weaponsData = _weaponSaveData.NonByedWeapon;
        _buyAndSelectButtone.onClick.RemoveAllListeners();
        _buyAndSelectButtone.onClick.AddListener(BuyWeapon);
        _buyAndSelectButtoneText.text = "Buy";
        _moneyText.text = _moneyShower.Money.ToString();

    }
    public void SetMenuAsSelectingMenu()
    {
        _weaponsData = _weaponSaveData.ByedWeapon;
        _buyAndSelectButtone.onClick.RemoveAllListeners();
        _buyAndSelectButtone.onClick.AddListener(SelectWeapone);
        _buyAndSelectButtoneText.text = "Select";
        _moneyText.text = _moneyShower.Money.ToString();
    }
    #endregion

    public void DeleteMenu()
    {
        foreach (var item in _weaponsMemmoryList)
        {
            Destroy(item.gameObject);
            _weaponsMemmoryList.Remove(item);
        }
        if (_newWeapon != null)
            Destroy(_newWeapon.gameObject);
        if (_oldWeapon != null)
            Destroy(_oldWeapon.gameObject);
        _newWeapon = null;
        _oldWeapon = null;
        _queuePosition = 0;
    }
    public void SelectWeapone()
    {
        _gun.EqipeNewGun(_weaponsData[_queuePosition - 1]);
        _weaponSaveData.SetNewDefaultGun(_weaponsData[_queuePosition - 1]);
    }
    public void BuyWeapon()
    {
        _weaponSaveData.ByingWeapon(_weaponsData[_queuePosition - 1]);
    }



    private void Update()
    {
        if (_time < 1.2f)
            _time += Time.deltaTime;
    }

}