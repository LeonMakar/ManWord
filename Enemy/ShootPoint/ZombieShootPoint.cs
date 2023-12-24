using UnityEngine;

public class ZombieShootPoint : MonoBehaviour,IShootable
{
    [SerializeField] protected Zombie Zombie;
    [SerializeField, Range(0.2f, 5)] protected float DamageModificator = 1;

    public void Attacked(int damageValue) => Zombie.GetDamage(Mathf.FloorToInt(damageValue * DamageModificator));

    public void InvokeOnUnderAimAction() => Zombie.OnUnderAim();
}

