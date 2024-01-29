using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNPC : MonoBehaviour
{
    private Transform _startTransfrom;
    private void Awake()
    {
        _startTransfrom = transform;
    }


    private void OnEnable()
    {
        transform.position = _startTransfrom.position;
    }
}
