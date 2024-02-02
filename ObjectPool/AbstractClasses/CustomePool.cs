using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CustomePool<T> where T : MonoBehaviour
{
    private List<T> _gameObjcetsList;
    private IFactory _factory;
    private bool _isEnemyPool;


    public CustomePool(IFactory factory, int initObjectsCount, bool isEnemyPool)
    {
        _isEnemyPool = isEnemyPool;
        _gameObjcetsList = new List<T>();
        _factory = factory;
        for (int i = 0; i < initObjectsCount; i++)
        {
            if (!_isEnemyPool)
            {
                var prefabGameObject = _factory.Create();
                prefabGameObject.GameObject().SetActive(false);
                _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
            }
            else
            {
                var prefabGameObject = _factory.Create<T>();
                prefabGameObject.GameObject().SetActive(false);
                _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
            }
        }
        _isEnemyPool = isEnemyPool;
    }
    public void RemooveAllObject()
    {
        var gameObjecs = _gameObjcetsList.FindAll(x => x.isActiveAndEnabled);
        foreach (var gameObj in gameObjecs)
        {
            DropBackToPool(gameObj);
        }
    }

    public T GetFromPool()
    {
        var gameObject = _gameObjcetsList.FirstOrDefault(x => !x.isActiveAndEnabled);
        if (gameObject == null)
            gameObject = CreateGameObjectForPool();
        gameObject.gameObject.SetActive(true);
        return gameObject;
    }

    public void DropBackToPool(T gameObject) => gameObject.gameObject.SetActive(false);

    private T CreateGameObjectForPool()
    {
        if (!_isEnemyPool)
        {
            var prefabGameObject = _factory.Create();
            prefabGameObject.GameObject().SetActive(false);
            _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
            return prefabGameObject.GetComponent<T>();

        }
        else
        {
            var prefabGameObject = _factory.Create<T>();
            prefabGameObject.GameObject().SetActive(false);
            _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
            return prefabGameObject.GetComponent<T>();
        }
    }
}
