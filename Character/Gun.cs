using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;
using YG.Example;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform _bulletAimPoint;
    [Space(15)]
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private ParticleSystem _shootParticle;
    [Space(15)]

    [SerializeField] private GameObject _bulletTrail;
    [SerializeField] private float _shootDelay = 1.0f;
    [SerializeField] private Vector3 _gunSpred;
    [Space(15)]

    [SerializeField] private LayerMask _hitableLayer;
    private bool _isReloading = false;
    [SerializeField] private float _rateOfFire;
    private int _gunCount = 0;
    [SerializeField] TrailRenderer _trail;

    private int _maxDistance = 60;

    private void Awake()
    {
        StartCoroutine(Shooting(_rateOfFire));
    }
    public void Shoot()
    {

        StartCoroutine(SpawnShoot(GetSprededPoint(_bulletAimPoint.position)));
        Ray ray = new Ray(_bulletSpawnPoint.position, transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(_bulletSpawnPoint.position, transform.TransformDirection(Vector3.forward) * 150, Color.red, 2);
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue, _hitableLayer))
        {
            _gunCount++;
            //Debug.Log($"Попал {hit.transform.position}" + _gunCount);
            Debug.Log(hit.normal);

        }
    }
    private void Update()
    {
        FindTargetForAimPoint();
    }
    private void FindTargetForAimPoint()
    {
        Ray ray = new Ray(_bulletSpawnPoint.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue, LayerMask.GetMask("Default", "Hitable")))
        {
            float distance = Vector3.Distance(_bulletSpawnPoint.position, hit.point);
            float scale = Mathf.Lerp(1, 4, distance / _maxDistance);
            _bulletAimPoint.localScale = new Vector3(scale, scale, scale);;
            _bulletAimPoint.position = hit.point;
        }
    }
    private IEnumerator Shooting(float rateOfFire)
    {
        while (true)
        {
            if (!_isReloading)
            {
                yield return new WaitForSeconds(rateOfFire);
                Shoot();
            }

        }
    }
    private Vector3 GetSprededPoint(Vector3 direction)
    {
        direction.x = UnityEngine.Random.Range(direction.x - _gunSpred.x, direction.x + _gunSpred.x);
        direction.y = UnityEngine.Random.Range(direction.y - _gunSpred.y, direction.y + _gunSpred.y);
        direction.z = UnityEngine.Random.Range(direction.z - _gunSpred.z, direction.z + _gunSpred.z);

        return direction;
    }
    private IEnumerator SpawnShoot(Vector3 shootPoint)
    {

        float time = 0;
        TrailRenderer trailRenderer = Instantiate(_trail, _trail.transform.position, Quaternion.identity);
        trailRenderer.enabled = true;
        while (time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(_bulletTrail.transform.position, shootPoint, time);
            time += Time.deltaTime / _trail.time;
            yield return null;
        }
        Destroy(trailRenderer.gameObject);
    }
}
