using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObstacleSpawner : Spawner
{
    private BonusePool _bonusPool;
    [SerializeField] private ChanceRange _chanceOfSimpleObstacle;

    private GameObject _spawnObject;

    [Inject]
    private void Construct(BonusePool pool)
    {
        _bonusPool = pool;
    }
    protected override IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTimeRate);
            if (GameIsActive)
            {
                int chance = UnityEngine.Random.Range(0, 100);
                float x = RandomizeXPosition();

                if (chance >= _chanceOfSimpleObstacle.start && chance < _chanceOfSimpleObstacle.end)
                    _spawnObject = _bonusPool.GetSimpleObstacleNegativeBonus().GetFromPool().gameObject;

                _spawnObject.gameObject.SetActive(false);
                _spawnObject.transform.position = new Vector3(x, _spawnObject.transform.position.y, transform.position.z);
                _spawnObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
                _spawnObject.gameObject.SetActive(true);
            }
        }
    }
}