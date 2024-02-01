[System.Serializable]
public class GunDataSave
{
    public string GunName;
    public int Damage;
    public float RateOfFire;
    public float GunSpred;
    public int BulletAmmount;
    public float ReloadingTime;

    public int DamageUpgradeCost;
    public int RateOfFireUpgradeCost;
    public int SpredUpgradeCost;
    public int AmmoUpgradeCost;
    public int ReloadingUpgradeCost;

    public GunDataSave Init(GunData gunData)
    {
        GunName = gunData.GunName;
        Damage = gunData.Damage;
        RateOfFire = gunData.RateOfFire;
        GunSpred = gunData.GunSpred;
        BulletAmmount = gunData.BulletAmmount;
        ReloadingTime = gunData.ReloadingTime;

        DamageUpgradeCost= gunData.DamageUpgradeCost;
        RateOfFireUpgradeCost= gunData.RateOfFireUpgradeCost;
        SpredUpgradeCost = gunData.SpredUpgradeCost;
        AmmoUpgradeCost= gunData.AmmoUpgradeCost;
        ReloadingUpgradeCost= gunData.ReloadingUpgradeCost;

        return this;
    }
}
