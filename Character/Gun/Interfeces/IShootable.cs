using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IShootable
{
    void Attacked(int damageValue);
    void InvokeOnUnderAimAction();
}

