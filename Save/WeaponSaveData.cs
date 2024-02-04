using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class WeaponSaveData : MonoBehaviour
{
    public List<GunData> ByedWeapon = new List<GunData>();
    public List<GunData> NonByedWeapon = new List<GunData>();
    public GunData DefoltGun;

    private Gun _gun;
    private UIMoneyShower _money;

    [Inject]
    private void Construct(Gun gun, UIMoneyShower money)
    {
        _gun = gun;
        _money = money;
    }
    public void ByingWeapon(GunData weaponData, PurchaseType purchaseType)
    {
        ByedWeapon.Add(weaponData);
        NonByedWeapon.Remove(weaponData);
        if (purchaseType == PurchaseType.Money)
            _money.AllMoney -= weaponData.GunCoast;
        else
            _money.AllGold -= weaponData.GunCoast;
    }

    public void Loading()
    {
        _gun.EqipeNewGun(DefoltGun);
    }

    public List<GunData> GetAllByedWeapon() => ByedWeapon;
    public void SetNewDefaultGun(GunData weaponData)
    {
        _gun.EqipeNewGun(weaponData);
        DefoltGun = weaponData;
    }
}
