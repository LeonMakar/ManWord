using Zenject;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "ScriptData/EnemyPool")]
public class EnemyPool : ScriptableObject
{
    private CustomePool<SimpleZomby> _simpleZombyPool;
    private CustomePool<YeakAndFastZomby> _yeakAndFastZombyPool;
    private CustomePool<FastZombie> _fastZombyPool;
    private CustomePool<StrongZombie> _strongZombyPool;
    private CustomePool<SuperStrongZombie> _superStrongZombyPool;



    public CustomePool<SimpleZomby> GetSimpleZombyPool() => _simpleZombyPool;
    public CustomePool<YeakAndFastZomby> GetYeakAndFastZombiePool() => _yeakAndFastZombyPool;
    public CustomePool<FastZombie> GetFastZombiePool() => _fastZombyPool;
    public CustomePool<StrongZombie> GetStrongZombiePool() => _strongZombyPool;
    public CustomePool<SuperStrongZombie> GetSuperStrongZombie() => _superStrongZombyPool;
    public void Initialize(IFactory enemyFactory)
    {
        _simpleZombyPool = new CustomePool<SimpleZomby>(enemyFactory, 5, true);
        _yeakAndFastZombyPool = new CustomePool<YeakAndFastZomby>(enemyFactory, 5, true);
        _fastZombyPool = new CustomePool<FastZombie>(enemyFactory, 2, true);
        _strongZombyPool = new CustomePool<StrongZombie>(enemyFactory, 1, true);
        _superStrongZombyPool = new CustomePool<SuperStrongZombie>(enemyFactory, 1, true);
    }



    public void RemooveAllEnemyFromScene()
    {
        _simpleZombyPool.RemooveAllObject();
        _yeakAndFastZombyPool.RemooveAllObject();
        _fastZombyPool.RemooveAllObject();
        _strongZombyPool.RemooveAllObject();
        _superStrongZombyPool.RemooveAllObject();
    }
}
