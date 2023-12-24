using UnityEngine;
using Zenject;

public class EnemyPoolInitialization : MonoBehaviour
{
    [SerializeField] EnemyPool _enemyPool;

    private IEnemyFactory enemyFactory;

    [Inject]
    public void Construct(IEnemyFactory simpleZombyFactory)
    {
        enemyFactory = simpleZombyFactory;
        enemyFactory.Load();
    }
    private void Start()
    {
        _enemyPool.Initialize(enemyFactory);
    }
}
