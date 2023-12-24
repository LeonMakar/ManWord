using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IEnemyFactory
{
    void Load();
    Object Create();
}