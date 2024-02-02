using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawner : Spawner
{
    private EnemyPool _enemyPool;
    [SerializeField] private EnemySpawnChanceData _spawnData;



    [Inject]
    private void Construct(EnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
    }

    public void ChangeSpawnData(EnemySpawnChanceData spawnData) => _spawnData = spawnData;

    protected override IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnData.SpawnRate);
            if (GameIsActive)
            {
                int chance = Random.Range(0, 100);

                if (chance >= _spawnData.SimpleZombSpawning.start && chance < _spawnData.SimpleZombSpawning.end)
                {
                    var enemy = _enemyPool.GetSimpleZombyPool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                if (chance >= _spawnData.YeakAndFastZombSpawning.start && chance < _spawnData.YeakAndFastZombSpawning.end)
                {
                    var enemy = _enemyPool.GetYeakAndFastZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                if (chance >= _spawnData.FastZombSpawning.start && chance < _spawnData.FastZombSpawning.end)
                {
                    var enemy = _enemyPool.GetFastZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                if (chance >= _spawnData.StrongZombSpawning.start && chance < _spawnData.StrongZombSpawning.end)
                {
                    var enemy = _enemyPool.GetStrongZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                if (chance >= _spawnData.SuperStrongZombSpawning.start && chance < _spawnData.SuperStrongZombSpawning.end)
                {
                    var enemy = _enemyPool.GetSuperStrongZombie().GetFromPool();
                    InitializeEnemy(enemy);
                }
            }
            else
                yield return null;
        }
    }

    private void InitializeEnemy(Zombie enemy)
    {
        float x = RandomizeXPosition();
        enemy.gameObject.SetActive(false);
        enemy.transform.position = new Vector3(x, transform.position.y, transform.position.z);
        enemy.transform.rotation = Quaternion.LookRotation(Vector3.back);
        enemy.gameObject.SetActive(true);
        enemy.DoActionsAfterSpawning();
    }
}

