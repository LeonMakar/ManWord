using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class SaveAndLoadProcess : MonoBehaviour
{
    private UIMoneyShower _money;
    private WeaponSaveData _weaponSaveData;
    [SerializeField] private List<GunData> _allDefaultGuns;
    private Dictionary<string, GunData> _allGunDataDictionary = new Dictionary<string, GunData>();


    [Inject]
    private void Construct(UIMoneyShower money, WeaponSaveData weaponSaveData)
    {
        _money = money;
        _weaponSaveData = weaponSaveData;
        AllGunDataDictionaryInitialization();
    }
    private void Awake()
    {
        YandexGame.GetDataEvent += Initialize;
    }
    private void AllGunDataDictionaryInitialization()
    {
        foreach (var gun in _allDefaultGuns)
            _allGunDataDictionary.Add(gun.GunName, gun);
    }

    private void Initialize()
    {
        LoadGameData();
    }

    public void SaveGameData()
    {
        YandexGame.savesData.Money = _money.AllMoney;
        YandexGame.savesData.Gold = _money.AllGold;

        SaveGunShopAssortment();
        YandexGame.savesData.IsFirstSwitchingOn = false;
        YandexGame.SaveProgress();
    }


    public void LoadGameData()
    {
        if (!YandexGame.savesData.IsFirstSwitchingOn)
            LoadGunShopAssortment();

        _money.Loading();
        _weaponSaveData.Loading();
    }

    private void LoadGunShopAssortment()
    {
        _weaponSaveData.DefoltGun = _allGunDataDictionary[YandexGame.savesData.DefoltGun.GunName];

        {
            _weaponSaveData.NonByedWeapon = new List<GunData>();
            foreach (var gun in YandexGame.savesData.NonByedGuns)
                _weaponSaveData.NonByedWeapon.Add(_allGunDataDictionary[gun.GunName].Init(gun));
        }
        {
            _weaponSaveData.ByedWeapon = new List<GunData>();
            foreach (var gun in YandexGame.savesData.ByedGuns)
                _weaponSaveData.ByedWeapon.Add(_allGunDataDictionary[gun.GunName].Init(gun));
        }
    }

    private void SaveGunShopAssortment()
    {
        YandexGame.savesData.DefoltGun = new GunDataSave().Init(_weaponSaveData.DefoltGun);
        {
            YandexGame.savesData.NonByedGuns = new List<GunDataSave>();
            foreach (var gunData in _weaponSaveData.NonByedWeapon)
                YandexGame.savesData.NonByedGuns.Add(new GunDataSave().Init(gunData));
        }
        {
            YandexGame.savesData.ByedGuns = new List<GunDataSave>();
            foreach (var gunData in _weaponSaveData.ByedWeapon)
                YandexGame.savesData.ByedGuns.Add(new GunDataSave().Init(gunData));
        }
    }
}
