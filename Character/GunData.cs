using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun/GunData"), Serializable]
public class GunData : ScriptableObject
{

    [SerializeField] private int _damageDefault;
    [SerializeField] private float _rateOfFireDefault;
    [SerializeField] private float _gunSpredDefault;
    [SerializeField] private int _bulletAmmountDefault;
    [SerializeField] private float _reloadingTimeDefault;

    public readonly ReactiveProperty<int> Damage = new();
    public readonly ReactiveProperty<float> RateOfFire = new();
    public readonly ReactiveProperty<float> GunSpred = new();
    public readonly ReactiveProperty<int> BulletAmmount = new();
    public readonly ReactiveProperty<float> ReloadingTime = new();


    [field: SerializeField] public string GunName { get; private set; }

    public PurchaseType PurchaseType;
    [field: SerializeField] public int GunCoast { get; private set; }
    [field: SerializeField] public AudioClip ShootSound { get; private set; }
    [field: SerializeField] public AudioClip ReloadingSound { get; private set; }
    [field: SerializeField] public GameObject GunPrefab { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    [Space(10), Header("Upgrade values")]
    public int DamageUpgradeValue;
    public float RateOfFireUpgradeValue;
    public float SpredUpgradeValue;
    public int BulletAmmountUpgradeValue;
    public float ReloadingTImeUpgradeValue;

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

    public void LoadDefaultParameters()
    {
        Damage.Value = _damageDefault;
        RateOfFire.Value = _rateOfFireDefault;
        GunSpred.Value = _gunSpredDefault;
        BulletAmmount.Value = _bulletAmmountDefault;
        ReloadingTime.Value = _reloadingTimeDefault;

    }
    public GunData LoadSavedParameters(GunDataSave save)
    {
        Damage.Value = save.Damage;
        RateOfFire.Value = save.RateOfFire;
        GunSpred.Value = save.GunSpred;
        BulletAmmount.Value = save.BulletAmmount;
        ReloadingTime.Value = save.ReloadingTime;

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
