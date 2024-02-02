using UnityEngine;

public interface IFactory
{
    void Load();
    Object Create();
    Object Create<TZombieImplamentation>();

}
