using UnityEngine;
using YG;
using Zenject;

public class SaveAndLoadProcess : MonoBehaviour
{
    private UIMoneyShower _money;
    private WeaponSaveData _weaponSaveData;

    [Inject]
    private void Construct(UIMoneyShower money, WeaponSaveData weaponSaveData)
    {
        _money = money;
        _weaponSaveData = weaponSaveData;

        YandexGame.GetDataEvent += Initialize;
    }

    private void Initialize()
    {
        LoadGameData();
    }

    public void SaveGameData()
    {
        YandexGame.savesData.IsFirstSwitchingOn = false;
        YandexGame.savesData.Money = _money.Money;
        SaveGunShopAssortment();

        YandexGame.SaveProgress();
    }


    public void LoadGameData()
    {
        _money.Loading();
        if (!YandexGame.savesData.IsFirstSwitchingOn)
            LoadGunShopAssortment();
    }

    private void LoadGunShopAssortment()
    {
        _weaponSaveData.ByedWeapon = YandexGame.savesData.ByedGuns;
        _weaponSaveData.NonByedWeapon = YandexGame.savesData.NonByedGuns;
        _weaponSaveData.DefoltGun = YandexGame.savesData.DefoltGun;
    }

    private void SaveGunShopAssortment()
    {
        YandexGame.savesData.DefoltGun = _weaponSaveData.DefoltGun;
        YandexGame.savesData.NonByedGuns = _weaponSaveData.NonByedWeapon;
        YandexGame.savesData.ByedGuns = _weaponSaveData.ByedWeapon;
    }
}
