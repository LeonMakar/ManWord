using Zenject;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "ScriptData/EnemyPool")]
public class EnemyPool : ScriptableObject
{
    [SerializeField] private SimpleZomby _simpleZombyPrefab;

    private CustomePool<SimpleZomby> _simpleZombyPool;

    public CustomePool<SimpleZomby> GetSimpleZombyPool() => _simpleZombyPool;

    public void Initialize(IFactory enemyFactory)
    {
        _simpleZombyPool = new CustomePool<SimpleZomby>(enemyFactory, 10);
    }



    public void RemooveAllEnemyFromScene()
    {
        _simpleZombyPool.RemooveAllObject();
    }
}
