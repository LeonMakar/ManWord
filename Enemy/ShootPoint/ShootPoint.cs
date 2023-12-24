using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    [SerializeField] protected Zombie Zombie;
    [SerializeField, Range(0.2f, 5)] protected float DamageModificator = 1;

    public void Attacked(int damageValue) => Zombie.GetDamage( Mathf.FloorToInt(damageValue * DamageModificator));

    public void InvokeOnUnderAimAction() => Zombie.OnUnderAim();
}
