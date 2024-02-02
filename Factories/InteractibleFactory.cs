using UnityEngine;

public class IntreractibleFactory<T> : IFactory where T : InteractibleObjects
{
    private T _prefab;

    public IntreractibleFactory(T prefab)
    {
        _prefab = prefab;
    }

    public Object Create()
    {
        Object gameObject = GameObject.Instantiate(_prefab);
        return gameObject;
    }

    public Object Create<TZombieImplamentation>()
    {
        throw new System.NotImplementedException();
    }

    public void Load()
    {

    }
}
