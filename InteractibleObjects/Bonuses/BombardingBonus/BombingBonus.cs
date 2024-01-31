using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingBonus : InteractibleObjects
{
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private int _rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            GameConstans.Bombarding.Invoke();
            gameObject.SetActive(false);
        }
    }

    public override void Update()
    {
        base.Update();
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
}
