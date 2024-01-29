using UnityEngine;
using Zenject;

public class PoolInitialization : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BonusePool _bonusPool;

    private IFactory enemyFactory;

    [Inject]
    public void Construct(IFactory simpleZombyFactory)
    {
        enemyFactory = simpleZombyFactory;
        enemyFactory.Load();
    }
    private void Start()
    {
        _enemyPool.Initialize(enemyFactory);
        _bonusPool.Initialize();
    }
}
