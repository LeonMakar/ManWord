using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunParticlesPool
{
    private ParticleSystem _prefab;
    private List<ParticleSystem> _gameObjcetsList;


    public GunParticlesPool(ParticleSystem prefab, int initObjectsCount)
    {
        _gameObjcetsList = new List<ParticleSystem>();
        _prefab = prefab;
        for (int i = 0; i < initObjectsCount; i++)
        {
            CreateGameObjectForPool();
        }
    }

    public ParticleSystem GetFromPool()
    {
        var gameObject = _gameObjcetsList.FirstOrDefault(x => !x.gameObject.activeSelf);
        if (gameObject == null)
            gameObject = CreateGameObjectForPool();
        gameObject.gameObject.SetActive(true);
        return gameObject;
    }

    public void DropBackToPool(ParticleSystem gameObject) => gameObject.gameObject.SetActive(false);

    private ParticleSystem CreateGameObjectForPool()
    {
        var prefabGameObject = GameObject.Instantiate(_prefab);
        prefabGameObject.gameObject.SetActive(false);
        _gameObjcetsList.Add(prefabGameObject);
        return prefabGameObject;
    }
}