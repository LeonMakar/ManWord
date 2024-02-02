using UnityEngine;
using Zenject;

public class EnemyFactory : IFactory
{
    private DiContainer _diContainer;

    private Object _simpleZomby;
    private Object _yeakAndFastZombie;
    private Object _fastZombie;
    private Object _strongZombie;
    private Object _superStrongZombie;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Load()
    {
        _simpleZomby = Resources.Load("SimpleZombie");
        _yeakAndFastZombie = Resources.Load("YeakAndFastZombie");
        _fastZombie = Resources.Load("FastZombie");
        _strongZombie = Resources.Load("StrongZombie");
        _superStrongZombie = Resources.Load("SuperStrongZombie");
    }
    public Object Create<TZombieImplamentation>()
    {
        switch (typeof(TZombieImplamentation).Name)
        {
            case nameof(SimpleZomby):
                return _diContainer.InstantiatePrefab(_simpleZomby);
            case nameof(YeakAndFastZomby):
                return _diContainer.InstantiatePrefab(_yeakAndFastZombie);
            case nameof(FastZombie):
                return _diContainer.InstantiatePrefab(_fastZombie);
            case nameof(StrongZombie):
                return _diContainer.InstantiatePrefab(_strongZombie);
            case nameof(SuperStrongZombie):
                return _diContainer.InstantiatePrefab(_superStrongZombie);
        }



        throw new System.NotImplementedException("Incorrect zombieType");

    }

    public Object Create()
    {
        throw new System.NotImplementedException("Incorrect Factory method");
    }
}
