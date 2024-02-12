using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun/GunData"), Serializable]
public class GunData : ScriptableObject
{
    [field: SerializeField] public string GunName { get; private set; }
    public int Damage;
    public float RateOfFire;
    public float GunSpred;
    public int BulletAmmount;
    public float ReloadingTime;
    public PurchaseType PurchaseType;
    [field: SerializeField] public int GunCoast { get; private set; }
    [field: SerializeField] public AudioClip ShootSound { get; private set; }
    [field: SerializeField] public AudioClip ReloadingSound { get; private set; }
    [field: SerializeField] public GameObject GunPrefab { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    [Space(10), Header("Upgrade values")]
    [SerializeField] private int _damageUpgradeValue;
    [SerializeField] private float _rateOfFireUpgradeValue;
    [SerializeField] private float _spredUpgradeValue;
    [SerializeField] private int _bulletAmmountUpgradeValue;
    [SerializeField] private float _reloadingTImeUpgradeValue;


    [Space(10), Header("Upgrade cost")]
    public int DamageUpgradeCost;
    public int RateOfFireUpgradeCost;
    public int SpredUpgradeCost;
    public int AmmoUpgradeCost;
    public int ReloadingUpgradeCost;

    [Space(10), Header("Upgrade step")]
    public int DamageUpStep;
    public float RateOfFireUpStep;
    public int SpredUpStep;
    public int AmmoUpStep;
    public int ReloadingUpStep;

    [field: SerializeField, Space(10)] public float CostMultiplyIndex { get; private set; }

    public void UpgradeDamage() => Damage += _damageUpgradeValue;
    public void UpgradeRateOfFire() => RateOfFire -= _rateOfFireUpgradeValue;
    public void UpgradeGunSpred() => GunSpred -= _spredUpgradeValue;
    public void UpgradeBulletAmmount() => BulletAmmount += _bulletAmmountUpgradeValue;
    public void UpgradeReloadingTime() => ReloadingTime -= _reloadingTImeUpgradeValue;

    public GunData Init(GunDataSave save)
    {
        Damage = save.Damage;
        RateOfFire = save.RateOfFire;
        GunSpred = save.GunSpred;
        BulletAmmount = save.BulletAmmount;
        ReloadingTime = save.ReloadingTime;

        DamageUpgradeCost = save.DamageUpgradeCost;
        RateOfFireUpgradeCost = save.RateOfFireUpgradeCost;
        SpredUpgradeCost = save.SpredUpgradeCost;
        AmmoUpgradeCost = save.AmmoUpgradeCost;
        ReloadingUpgradeCost = save.ReloadingUpgradeCost;

        DamageUpStep = save.DamageUpStep;
        RateOfFireUpStep = save.RateOfFireUpStep;
        SpredUpStep = save.SpredUpStep;
        AmmoUpStep = save.AmmoUpStep;
        ReloadingUpStep = save.ReloadingUpStep;

        return this;
    }

}

public enum PurchaseType
{
    Gold,
    Money
}
