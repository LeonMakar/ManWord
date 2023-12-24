using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour, IInjectable
{
    [SerializeField] private Vector2 _spawnBordersPosition;
    [SerializeField] private float _simpleZombieSpawnTime;


    private EnemyPool _enemyPool;



    [Inject]
    private void Construct(EnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
    }

    private void Start()
    {
        StartCoroutine(SpawnSimpleZombie());
    }

    private IEnumerator SpawnSimpleZombie()
    {
        while (true)
        {
            yield return new WaitForSeconds(_simpleZombieSpawnTime);

            var enemy = _enemyPool.GetSimpleZombyPool().GetFromPool();
            float x = RandomizeXPosition();
            enemy.gameObject.SetActive(false);
            enemy.transform.position = new Vector3(x, transform.position.y, transform.position.z);
            enemy.gameObject.SetActive(true);

        }
    }

    private float RandomizeXPosition()
    {
        float x = UnityEngine.Random.Range(_spawnBordersPosition.x, _spawnBordersPosition.y);
        return x;
    }
}
