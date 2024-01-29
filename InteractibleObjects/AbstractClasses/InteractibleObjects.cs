using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObjects : MonoBehaviour
{
    [SerializeField] protected GameObject ParentObject;
    [SerializeField] private int _speed;

    /// <summary>
    /// Moove Forward current Interactible object
    /// </summary>
    public virtual void Update()
    {
        ParentObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
