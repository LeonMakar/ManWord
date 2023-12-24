using System.Collections.Generic;
using UnityEngine;

public class AreaOfAction : MonoBehaviour
{
    private List<GameObject> _objects;
    private void Awake()
    {
        _objects = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _objects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _objects.Remove(other.gameObject);
    }

    public List<GameObject> GetAllObjectsInArea()
    {
        return _objects;
    }
}
