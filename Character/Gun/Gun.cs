using System;
using System.Collections;
using UnityEngine;
using YG.Example;
using Zenject;

public class Gun : MonoBehaviour, IService, IInjectable
{
    [SerializeField] private Transform _rawSpawnPoint;
    [SerializeField] private Transform _rawEndPoint;
    [SerializeField] private Transform _bulletAimPoint;
    [SerializeField] private Transform _headPoint;
    [SerializeField] private Transform _bulletSpawnPoint;


    [Space(15)]
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private ParticleSystem _bulletParticle;

    [Space(15)]
    [SerializeField] private float _shootDelay = 1.0f;
    [SerializeField] private Vector3 _gunSpred;
    [Space(15)]

    [SerializeField] private LayerMask _hitableLayer;
    [SerializeField] private float _rateOfFire;
    [SerializeField] private int _gunDamage = 10;
    [Space(15)]

    [SerializeField] private AudioSource _audioSource;

    private int _maxDistance = 60;
    private bool _isReloading = false;
    private bool _isMooving = false;
    private MainPlayerController _mainPlayerController;
    private GunTrail _gunTrail;
    private EventBus _eventBus;

    [Inject]
    private void Construct(MainPlayerController mainPlayerController, GunTrail gunTrail, EventBus eventBus)
    {
        _mainPlayerController = mainPlayerController;
        _gunTrail = gunTrail;
        _eventBus = eventBus;

    }

    private void Awake()
    {
        StartCoroutine(Shooting(_rateOfFire));
    }

    private void Update()
    {
        FindTargetForAimPoint();
    }
    public void UpgradeRateOfFire()
    {
        StopCoroutine(Shooting(_rateOfFire));
        StartCoroutine(Shooting(_rateOfFire));
    }
    public void Shoot()
    {
        _mainPlayerController.AnimateShoot();
        InstantiateParticlesAfterShoot();
        _gunTrail.SetShootPoint(GetSprededPoint(_bulletAimPoint.position));
        PlayShootAudio();
        Ray ray = CreateRay();
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue, _hitableLayer))
        {
            hit.transform.TryGetComponent(out IShootable shootableObject);
            shootableObject?.Attacked(_gunDamage);
            InstantiateHitParticles(hit);
        }
    }

    private void InstantiateHitParticles(RaycastHit hit)
    {
        var impactParticle = Instantiate(_impactParticle, hit.transform.position, Quaternion.identity);
        StartCoroutine(DeleteShootParticles(impactParticle, impactParticle.main.duration));
    }

    private Ray CreateRay()
    {
        var direction = GetSprededPoint(_rawEndPoint.position) - _rawSpawnPoint.position;
        Ray ray = new Ray(_rawSpawnPoint.position, direction);
        Debug.DrawRay(_rawSpawnPoint.position, direction * 150, Color.red, 2);
        return ray;
    }

    private void PlayShootAudio()
    {
        if (Time.timeScale == 1)
            _audioSource.pitch = UnityEngine.Random.Range(0.7f, 1);
        else
            _audioSource.pitch = UnityEngine.Random.Range(0.2f, 0.4f);
        _audioSource.Play();
    }

    private void InstantiateParticlesAfterShoot()
    {
        var shootParticle = Instantiate(_shootParticle, _bulletSpawnPoint);
        StartCoroutine(DeleteShootParticles(shootParticle, shootParticle.main.duration));
        var bulletParticle = Instantiate(_bulletParticle, _bulletParticle.transform.position, _bulletParticle.transform.rotation);
        StartCoroutine(DeleteShootParticles(bulletParticle, bulletParticle.main.duration));
    }

    private IEnumerator DeleteShootParticles(ParticleSystem particle, float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        Destroy(particle.gameObject);
    }
    public void PlayerMooves() => _isMooving = true;
    public void PlayerStopMooves() => _isMooving = false;
    private void FindTargetForAimPoint()
    {
        Ray ray = new Ray(_headPoint.position, _rawSpawnPoint.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue, LayerMask.GetMask("Default", "Hitable")))
        {
            if (hit.collider.transform.gameObject.layer == 6)
            {
                _bulletAimPoint.GetComponentInChildren<Renderer>().material.color = Color.red;
            }
            else
                _bulletAimPoint.GetComponentInChildren<Renderer>().material.color = Color.green;

            ResizeAimPoint(hit);

            hit.transform.TryGetComponent<ZombieShootPoint>(out ZombieShootPoint shootPoint);
            shootPoint?.InvokeOnUnderAimAction();
        }

    }

    private void ResizeAimPoint(RaycastHit hit)
    {
        float distance = Vector3.Distance(_rawSpawnPoint.position, hit.point);
        float scale = Mathf.Lerp(1, 4, distance / _maxDistance);
        _bulletAimPoint.localScale = new Vector3(scale, scale, scale); ;
        _bulletAimPoint.position = hit.point;
    }

    private IEnumerator Shooting(float rateOfFire)
    {
        while (true)
        {
            if (!_isReloading && !_isMooving)
            {
                yield return new WaitForSeconds(rateOfFire);
                if (!_isMooving && !_isReloading)
                    Shoot();
            }
            yield return null;
        }
    }
    private Vector3 GetSprededPoint(Vector3 direction)
    {
        direction.x = UnityEngine.Random.Range(direction.x - _gunSpred.x, direction.x + _gunSpred.x);
        direction.y = UnityEngine.Random.Range(direction.y - _gunSpred.y, direction.y + _gunSpred.y);
        direction.z = UnityEngine.Random.Range(direction.z - _gunSpred.z, direction.z + _gunSpred.z);

        return direction;
    }
}


