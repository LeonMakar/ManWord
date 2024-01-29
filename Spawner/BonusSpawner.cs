using System.Collections;
using UnityEngine;
using Zenject;

public class BonusSpawner : Spawner
{
    [SerializeField] private ChanceRange _chanceOfExplosion;
    [SerializeField] private ChanceRange _chanceOfAiming;
    [SerializeField] private ChanceRange _chanceOfDoubleDamage;
    [SerializeField] private ChanceRange _chanceOfMoney;

    private BonusePool _bonusPool;
    private GameObject _spawnObject;


    [Inject]
    private void Construct(BonusePool bonusePool)
    {
        _bonusPool = bonusePool;
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

                if (chance >= _chanceOfAiming.start && chance < _chanceOfAiming.end)
                    _spawnObject = _bonusPool.GetAimingBonusPool().GetFromPool().gameObject;
                if (chance >= _chanceOfExplosion.start && chance < _chanceOfExplosion.end)
                    _spawnObject = _bonusPool.GetExpolisiveBonusPool().GetFromPool().gameObject;
                if (chance >= _chanceOfDoubleDamage.start && chance < _chanceOfDoubleDamage.end)
                    _spawnObject = _bonusPool.GetDoubleDamagePool().GetFromPool().gameObject;
                if (chance >= _chanceOfMoney.start && chance < _chanceOfMoney.end)
                    _spawnObject = _bonusPool.GetMoneyPool().GetFromPool().gameObject;

                _spawnObject.gameObject.SetActive(false);
                _spawnObject.transform.position = new Vector3(x, _spawnObject.transform.position.y, transform.position.z);
                _spawnObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
                _spawnObject.gameObject.SetActive(true);
            }
        }
    }
}


