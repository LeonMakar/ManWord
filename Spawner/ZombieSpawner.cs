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

    protected override IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTimeRate);
            if (GameIsActive)
            {
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
}

