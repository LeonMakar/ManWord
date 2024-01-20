using UnityEngine;
using Zenject;

public class EnemyPoolInitialization : MonoBehaviour
{
    [SerializeField] EnemyPool _enemyPool;

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
    }
}
