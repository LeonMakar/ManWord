using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBonus : InteractibleObjects, IShootable
{
    [SerializeField] private AreaOfAction _areaOfAction;
    public void Attacked(int damageValue)
    {
        Debug.Log("����� ");
        foreach (var gameObject in _areaOfAction.GetAllObjectsInArea())
        {

        }
    }

    public void InvokeOnUnderAimAction()
    {
        
    }
}
