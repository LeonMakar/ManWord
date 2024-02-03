using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnChanceData", menuName = "ScriptData/ChanceOfSpawning"), Serializable]
public class EnemySpawnChanceData : ScriptableObject
{
    [field: SerializeField] public float SpawnRate { get; private set; }


  
    [field: SerializeField,Space(40)] public ChanceRange SimpleZombSpawning { get; private set; }
    [field: SerializeField] public ChanceRange YeakAndFastZombSpawning { get; private set; }
    [field: SerializeField] public ChanceRange FastZombSpawning { get; private set; }
    [field: SerializeField] public ChanceRange StrongZombSpawning { get; private set; }
    [field: SerializeField] public ChanceRange SuperStrongZombSpawning { get; private set; }
}

