using System.Collections;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour, IInjectable
{
    [SerializeField] private Vector2 _spawnBordersPosition;
    [SerializeField] private float _simpleZombieSpawnTime;


    private EnemyPool _enemyPool;
    private EventBus _eventBus;

    private bool _gameIsActive;

    [Inject]
    private void Construct(EnemyPool enemyPool, EventBus eventBus)
    {
        _enemyPool = enemyPool;
        _eventBus = eventBus;

        _eventBus.Subscrube<StartGameSignal>(SetGameActivity);
    }

    private void Start()
    {
        StartCoroutine(SpawnSimpleZombie());
    }

    private void SetGameActivity(StartGameSignal signal)
    {
        _gameIsActive = signal.GameIsStarted;
    }

    private IEnumerator SpawnSimpleZombie()
    {
        while (true)
        {
            if (_gameIsActive)
            {
                yield return new WaitForSeconds(_simpleZombieSpawnTime);

                var enemy = _enemyPool.GetSimpleZombyPool().GetFromPool();
                float x = RandomizeXPosition();
                enemy.gameObject.SetActive(false);
                enemy.transform.position = new Vector3(x, transform.position.y, transform.position.z);
                enemy.transform.rotation = Quaternion.LookRotation(Vector3.back);
                enemy.gameObject.SetActive(true);
                enemy.DoActionsAfterSpawning();
            }
            else
                yield return null;
        }
    }

    private float RandomizeXPosition()
    {
        float x = UnityEngine.Random.Range(_spawnBordersPosition.x, _spawnBordersPosition.y);
        return x;
    }
}
