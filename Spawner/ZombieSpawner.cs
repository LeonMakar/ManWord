using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawner : Spawner
{
    private EnemyPool _enemyPool;

    [Inject]
    private void Construct(EnemyPool enemyPool)
    {
        _enemyPool = enemyPool;
    }
    public void ChangeEnemySpawnData(EnemySpawnChanceData spawnData) => EnemySpawnData = spawnData;


    protected override IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemySpawnData.SpawnRate);
            if (GameIsActive)
            {
                int chance = Random.Range(0, 100);

                if (chance >= EnemySpawnData.SimpleZombSpawning.start && chance < EnemySpawnData.SimpleZombSpawning.end)
                {
                    var enemy = _enemyPool.GetSimpleZombyPool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                else if (chance >= EnemySpawnData.YeakAndFastZombSpawning.start && chance < EnemySpawnData.YeakAndFastZombSpawning.end)
                {
                    var enemy = _enemyPool.GetYeakAndFastZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                else if (chance >= EnemySpawnData.FastZombSpawning.start && chance < EnemySpawnData.FastZombSpawning.end)
                {
                    var enemy = _enemyPool.GetFastZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                else if (chance >= EnemySpawnData.StrongZombSpawning.start && chance < EnemySpawnData.StrongZombSpawning.end)
                {
                    var enemy = _enemyPool.GetStrongZombiePool().GetFromPool();
                    InitializeEnemy(enemy);
                }
                else if (chance >= EnemySpawnData.SuperStrongZombSpawning.start && chance < EnemySpawnData.SuperStrongZombSpawning.end)
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

