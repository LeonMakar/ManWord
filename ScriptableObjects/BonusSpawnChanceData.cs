using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusSpawnChanceData", menuName = "ScriptData/ChanceOfBonus"), Serializable]
public class BonusSpawnChanceData : ScriptableObject
{
    [field: SerializeField] public float BonusSpawnRate { get; private set; }
    [field: SerializeField] public float ObstacleBonusSpawnRate { get; private set; }




    [field: SerializeField] public ChanceRange ChanceOfExplosion { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfAiming { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfDoubleDamage { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfMoney { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfBombing { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfObstacle { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfSingleShield { get; private set; }
    [field: SerializeField] public ChanceRange ChanceOfDoubleShield { get; private set; }
}

