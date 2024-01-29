using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Vector2 SpawnBordersPosition;
    [SerializeField] protected float SpawnTimeRate;

    protected bool GameIsActive;

    public void StartSpawning()
    {
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
