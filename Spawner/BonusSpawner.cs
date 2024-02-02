using System.Collections;
using UnityEngine;
using Zenject;

public class BonusSpawner : Spawner
{
    [SerializeField] private ChanceRange ChanceOfExplosion;
    [SerializeField] private ChanceRange ChanceOfAiming;
    [SerializeField] private ChanceRange ChanceOfDoubleDamage;
    [SerializeField] private ChanceRange ChanceOfMoney;
    [SerializeField] private ChanceRange ChanceOfBombing;


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

                if (chance >= ChanceOfAiming.start && chance < ChanceOfAiming.end)
                    _spawnObject = _bonusPool.GetAimingBonusPool().GetFromPool().gameObject;
                if (chance >= ChanceOfExplosion.start && chance < ChanceOfExplosion.end)
                    _spawnObject = _bonusPool.GetExpolisiveBonusPool().GetFromPool().gameObject;
                if (chance >= ChanceOfDoubleDamage.start && chance < ChanceOfDoubleDamage.end)
                    _spawnObject = _bonusPool.GetDoubleDamagePool().GetFromPool().gameObject;
                if (chance >= ChanceOfMoney.start && chance < ChanceOfMoney.end)
                    _spawnObject = _bonusPool.GetMoneyPool().GetFromPool().gameObject;
                if (chance >= ChanceOfBombing.start && chance < ChanceOfBombing.end)
                    _spawnObject = _bonusPool.GetBombingPool().GetFromPool().gameObject;

                _spawnObject.gameObject.SetActive(false);
                _spawnObject.transform.position = new Vector3(x, _spawnObject.transform.position.y, transform.position.z);
                _spawnObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
                _spawnObject.gameObject.SetActive(true);
            }
        }
    }
}


