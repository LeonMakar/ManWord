using UnityEngine;
using Zenject;

public class PoolInitialization : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BonusePool _bonusPool;

    private IFactory _factory;

    [Inject]
    public void Construct(IFactory factory)
    {
        _factory = factory;
        _factory.Load();
    }
    private void Start()
    {
        _enemyPool.Initialize(_factory);
        _bonusPool.Initialize();
    }
}
