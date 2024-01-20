using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponSaveData : MonoBehaviour
{
    [field: SerializeField] public List<GunData> ByedWeapon { get; private set; } = new List<GunData>();
    [field: SerializeField] public List<GunData> NonByedWeapon { get; private set; } = new List<GunData>();

    [SerializeField] private GunData _defoltGun;

    private Gun _gun;

    [Inject]
    private void Construct(Gun gun)
    {
        _gun = gun;
        _gun.EqipeNewGun(_defoltGun);
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
        _defoltGun = weaponData;
    }
}
