using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Vector2 SpawnBordersPosition;
    protected EnemySpawnChanceData EnemySpawnData;
    protected BonusSpawnChanceData BonusSpawnData;


    protected bool GameIsActive;


    public void StartSpawning()
    {
        if (EnemySpawnData != null)
            Debug.Log(EnemySpawnData.ToString());
        if (BonusSpawnData != null)
            Debug.Log(BonusSpawnData.ToString());

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
