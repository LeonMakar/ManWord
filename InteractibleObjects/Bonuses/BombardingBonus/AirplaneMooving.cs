using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMooving : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _finishPosition;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private int _speed;




    private void Awake()
    {
        GameConstans.Bombarding += StartMooving;
    }

    private void StartMooving()
    {
        StartCoroutine(CalculatationLerp());
        _audio.Play();
    }
    private IEnumerator CalculatationLerp()
    {

        float distance = Vector3.Distance(_finishPosition.position, _startPosition.position);
        float remeningDistance = distance;
        while (remeningDistance > 0)
        {
            transform.position = Vector3.Lerp(_startPosition.position, _finishPosition.position, 1 - (remeningDistance / distance));
            remeningDistance -= _speed * Time.deltaTime;
            yield return null;
        }
        transform.position = _startPosition.position;
    }
}
