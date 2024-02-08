using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _rateOfFire;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Transform _trailStartPosition;
    [SerializeField] private Transform _turretRotationObject;

    [SerializeField] private MonoBehaviour _coroutineGameObject;
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private float _trailSpeed;

    private TrailPool _gunTrailPool;
    private Zombie _target;


    private void Start()
    {
        _gunTrailPool = new TrailPool(30, _trailPrefab);
        GameConstans.ActivateTurret += TurretActivations;
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == GameConstans.EnemyTag && _target == null)
        {
            other.transform.TryGetComponent(out Zombie target);
            if (!target.IsStartDying)
                _target = target;
        }
    }

    private void TurretActivations(bool boolian)
    {
        if (boolian)
            TuretIsActivated();
        else if (!boolian)
            TuretIsDiactivated();
    }
    private void TuretIsActivated()
    {
        StartCoroutine(Firing());
    }
    private void TuretIsDiactivated()
    {
        StopAllCoroutines();
    }

    private void DealDamage()
    {
        if (_target != null)
        {
            _audio.pitch = Random.Range(0.6f, 1);
            _audio.Play();
            _target.GetDamage(_damage);
            if (_target.GetHealthOfZombie() <= 0 || _target.IsStartDying)
                _target = null;
        }
    }

    private IEnumerator Firing()
    {
        while (true)
        {
            if (_target != null)
            {
                yield return new WaitForSeconds(_rateOfFire);
                _turretRotationObject.rotation = Quaternion.LookRotation(_target.transform.position - _turretRotationObject.position);
                _coroutineGameObject.StartCoroutine(StartFrowTrail(_target.transform.position + Vector3.up));
                DealDamage();
            }
            yield return null;
        }
    }

    private IEnumerator StartFrowTrail(Vector3 hitPosition)
    {
        Vector3 startPoint = _trailStartPosition.position + transform.TransformDirection(Vector3.forward);
        Vector3 finishPoint = hitPosition;
        float distance = Vector3.Distance(startPoint, finishPoint);
        float remeningDistance = distance;
        GameObject traailGameObject = _gunTrailPool.GetFromPool();
        traailGameObject.transform.position = startPoint;
        traailGameObject.transform.rotation = Quaternion.LookRotation(finishPoint - startPoint);
        while (remeningDistance > 0)
        {
            var trailPos = Vector3.Lerp(startPoint, finishPoint, 1 - (remeningDistance / distance));
            traailGameObject.transform.position = trailPos;
            remeningDistance -= _trailSpeed * Time.deltaTime;
            yield return null;
        }
        traailGameObject.SetActive(false);
    }
}
