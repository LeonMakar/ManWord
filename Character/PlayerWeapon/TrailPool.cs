using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailPool
{
    private GameObject _prefab;
    private List<GameObject> _list = new List<GameObject>();

    public TrailPool(int initObjectsCount, GameObject prefab)
    {
        _prefab = prefab;
        for (int i = 0; i < initObjectsCount; i++)
        {
            CreateGameObjectForPool();
        }
    }

    public GameObject GetFromPool()
    {
        var gameObject = _list.FirstOrDefault(x => !x.gameObject.activeSelf);
        if (gameObject == null)
            gameObject = CreateGameObjectForPool();
        gameObject.gameObject.SetActive(true);
        return gameObject;
    }

    public void DropBackToPool(ParticleSystem gameObject) => gameObject.gameObject.SetActive(false);

    private GameObject CreateGameObjectForPool()
    {
        var prefabGameObject = GameObject.Instantiate(_prefab);
        prefabGameObject.gameObject.SetActive(false);
        _list.Add(prefabGameObject);
        return prefabGameObject;
    }
}


