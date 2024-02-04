using System.Collections;
using UnityEngine;
using Zenject;

public class BonusSpawner : Spawner
{
    private BonusePool _bonusPool;
    private GameObject _spawnObject;

    public void ChangeBonusSpawnData(BonusSpawnChanceData spawnData) => BonusSpawnData = spawnData;

    [Inject]
    private void Construct(BonusePool bonusePool)
    {
        _bonusPool = bonusePool;
    }

    protected override IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(BonusSpawnData.BonusSpawnRate);
            if (GameIsActive)
            {
                int chance = UnityEngine.Random.Range(0, 100);
                float x = RandomizeXPosition();

                if (chance >= BonusSpawnData.ChanceOfAiming.start && chance < BonusSpawnData.ChanceOfAiming.end)
                    _spawnObject = _bonusPool.GetAimingBonusPool().GetFromPool().gameObject;
                else if (chance >= BonusSpawnData.ChanceOfExplosion.start && chance < BonusSpawnData.ChanceOfExplosion.end)
                    _spawnObject = _bonusPool.GetExpolisiveBonusPool().GetFromPool().gameObject;
                else if (chance >= BonusSpawnData.ChanceOfDoubleDamage.start && chance < BonusSpawnData.ChanceOfDoubleDamage.end)
                    _spawnObject = _bonusPool.GetDoubleDamagePool().GetFromPool().gameObject;
                else if (chance >= BonusSpawnData.ChanceOfMoney.start && chance < BonusSpawnData.ChanceOfMoney.end)
                    _spawnObject = _bonusPool.GetMoneyPool().GetFromPool().gameObject;
                else if (chance >= BonusSpawnData.ChanceOfBombing.start && chance < BonusSpawnData.ChanceOfBombing.end)
                    _spawnObject = _bonusPool.GetBombingPool().GetFromPool().gameObject;

                _spawnObject.gameObject.SetActive(false);
                _spawnObject.transform.position = new Vector3(x, _spawnObject.transform.position.y, transform.position.z);
                _spawnObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
                _spawnObject.gameObject.SetActive(true);
            }
        }
    }
}


