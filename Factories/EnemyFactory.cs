using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private DiContainer _diContainer;

    private Object _simpleZomby;
    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Load()
    {
        _simpleZomby = Resources.Load("SimpleZombie");
    }
    public Object Create()
    {
        return _diContainer.InstantiatePrefab(_simpleZomby);
    }
}
