using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CustomePool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _gameObjcetsList;
    private IEnemyFactory _factory;


    public CustomePool(IEnemyFactory factory, int initObjectsCount)
    {
        //_prefab = prefab;
        //_gameObjcetsList = new List<T>();
        //for (int i = 0; i < initObjectsCount; i++)
        //{
        //    var prefabGameObject = GameObject.Instantiate(_prefab);
        //    prefabGameObject.gameObject.SetActive(false);
        //    _gameObjcetsList.Add(prefabGameObject);
        //}
        _gameObjcetsList = new List<T>();
        _factory = factory;
        for (int i = 0; i < initObjectsCount; i++)
        {
            var prefabGameObject = _factory.Create();
            prefabGameObject.GameObject().SetActive(false);
            _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
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
        var prefabGameObject = _factory.Create();
        prefabGameObject.GameObject().SetActive(false);
        _gameObjcetsList.Add(prefabGameObject.GetComponent<T>());
        return prefabGameObject.GetComponent<T>();
    }
}
