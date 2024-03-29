using System.Collections;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Vector2 SpawnBordersPosition;
    protected EnemySpawnChanceData EnemySpawnData;
    protected BonusSpawnChanceData BonusSpawnData;


    protected bool GameIsActive;


    public void StartSpawning()
    {
        StopAllCoroutines();
        StartCoroutine(Spawning());
    }

    public void SetGameActivity(bool boolian)
    {
        GameIsActive = boolian;
    }

    protected abstract IEnumerator Spawning();

    protected float RandomizeXPosition()
    {
        float x = UnityEngine.Random.Range(SpawnBordersPosition.x, SpawnBordersPosition.y);
        return x;
    }
}
