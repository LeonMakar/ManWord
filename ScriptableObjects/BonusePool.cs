using UnityEngine;

[CreateAssetMenu(fileName = "BonusPool", menuName = "ScriptData/BonusPoll")]
public class BonusePool : ScriptableObject
{
    [SerializeField] private ExplosiveBonus _expolisiveBonus;
    [SerializeField] private AimingBonus _aimingBonus;
    [SerializeField] private DoubleDamageBonus _damageBonus;
    [SerializeField] private MoneyBonus _moneyBonus;
    [SerializeField] private BombingBonus _bombingBonus;
    [SerializeField] private SimpleObstacle _simpleObstacleNegativeBonus;
    [SerializeField] private ShieldObstacle _singleShieldNegativeBonus;
    [SerializeField] private ShieldObstacle _doubleShieldNegativeBonus;





    private CustomePool<ExplosiveBonus> _expolisiveBonusPool;
    private CustomePool<AimingBonus> _aimingBonusPool;
    private CustomePool<DoubleDamageBonus> _damageBonusPool;
    private CustomePool<MoneyBonus> _moneyBonusPool;
    private CustomePool<BombingBonus> _bombingBonusPool;
    private CustomePool<SimpleObstacle> _simpleObstacleNegativeBonusPool;
    private CustomePool<ShieldObstacle> _singleShieldNegativeBonusPool;
    private CustomePool<ShieldObstacle> _doubleShieldNegativeBonusPool;


    public CustomePool<ExplosiveBonus> GetExpolisiveBonusPool() => _expolisiveBonusPool;
    public CustomePool<AimingBonus> GetAimingBonusPool() => _aimingBonusPool;
    public CustomePool<DoubleDamageBonus> GetDoubleDamagePool() => _damageBonusPool;
    public CustomePool<MoneyBonus> GetMoneyPool() => _moneyBonusPool;
    public CustomePool<BombingBonus> GetBombingPool() => _bombingBonusPool;
    public CustomePool<SimpleObstacle> GetSimpleObstacleNegativeBonus() => _simpleObstacleNegativeBonusPool;
    public CustomePool<ShieldObstacle> GetSingleShieldNegativeBonus() => _singleShieldNegativeBonusPool;
    public CustomePool<ShieldObstacle> GetdoubleShieldNegativeBonus() => _doubleShieldNegativeBonusPool;


    public void Initialize()
    {
        _expolisiveBonusPool = new CustomePool<ExplosiveBonus>(new IntreractibleFactory<ExplosiveBonus>(_expolisiveBonus), 3, false);
        _aimingBonusPool = new CustomePool<AimingBonus>(new IntreractibleFactory<AimingBonus>(_aimingBonus), 3, false);
        _damageBonusPool = new CustomePool<DoubleDamageBonus>(new IntreractibleFactory<DoubleDamageBonus>(_damageBonus), 3, false);
        _moneyBonusPool = new CustomePool<MoneyBonus>(new IntreractibleFactory<MoneyBonus>(_moneyBonus), 3, false);
        _bombingBonusPool = new CustomePool<BombingBonus>(new IntreractibleFactory<BombingBonus>(_bombingBonus), 3, false);
        _simpleObstacleNegativeBonusPool = new CustomePool<SimpleObstacle>(new IntreractibleFactory<SimpleObstacle>(_simpleObstacleNegativeBonus), 3, false);
        _singleShieldNegativeBonusPool = new CustomePool<ShieldObstacle>(new IntreractibleFactory<ShieldObstacle>(_singleShieldNegativeBonus), 1, false);
        _doubleShieldNegativeBonusPool = new CustomePool<ShieldObstacle>(new IntreractibleFactory<ShieldObstacle>(_doubleShieldNegativeBonus), 1, false);
    }



    public void RemooveAllObjectFromScene()
    {
        _expolisiveBonusPool.RemooveAllObject();
        _aimingBonusPool.RemooveAllObject();
        _damageBonusPool.RemooveAllObject();
        _moneyBonusPool.RemooveAllObject();
        _bombingBonusPool.RemooveAllObject();
        _simpleObstacleNegativeBonusPool.RemooveAllObject();
        _singleShieldNegativeBonusPool.RemooveAllObject();
        _doubleShieldNegativeBonusPool.RemooveAllObject();
    }
}
