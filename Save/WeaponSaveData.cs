using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponSaveData : MonoBehaviour
{
    public List<GunData> ByedWeapon  = new List<GunData>();
    public List<GunData> NonByedWeapon  = new List<GunData>();
    public GunData DefoltGun;

    private Gun _gun;

    [Inject]
    private void Construct(Gun gun)
    {
        _gun = gun;
        _gun.EqipeNewGun(DefoltGun);
    }
    public void ByingWeapon(GunData weaponData)
    {
        ByedWeapon.Add(weaponData);
        NonByedWeapon.Remove(weaponData);
    }

    public List<GunData> GetAllByedWeapon() => ByedWeapon;
    public void SetNewDefaultGun(GunData weaponData)
    {
        _gun.EqipeNewGun(weaponData);
        DefoltGun = weaponData;
    }
}
