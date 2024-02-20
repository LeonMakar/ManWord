using UnityEngine;
using System.Collections;

public class GunParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private float _trailSpeed;
    [SerializeField] private GameObject _trailPrefab;

    public ParticleSystem ShootParticle { get; set; }
    public ParticleSystem BulletParticle { get; set; }

    private GunParticlesPool _gunBulletPool;
    private GunParticlesPool _gunShootSmokePool;
    private GunParticlesPool _gunHitPool;
    private TrailPool _gunTrailPool;
    public GameObject TrailGameObject { get; private set; }
    [SerializeField] private Coroutine _coroutineObject;


    public void InitializeGunParticlePools()
    {
        _gunBulletPool = new GunParticlesPool(BulletParticle, 20);
        _gunHitPool = new GunParticlesPool(_impactParticle, 20);
        _gunShootSmokePool = new GunParticlesPool(ShootParticle, 20);
        _gunTrailPool = new TrailPool(30, _trailPrefab);
    }

    public void ActivateParticlesAfterShoot()
    {
        //Smoke
        var smokeParticles = _gunShootSmokePool.GetFromPool();
        smokeParticles.transform.position = ShootParticle.transform.position;
        smokeParticles.Play();
        _coroutineObject.StartCoroutine(FinishParticleShown(smokeParticles, smokeParticles.main.duration));

        //Bullet
        var bulletParticle = _gunBulletPool.GetFromPool();
        bulletParticle.transform.position = BulletParticle.transform.position;
        bulletParticle.transform.rotation = BulletParticle.transform.rotation;
        _coroutineObject.StartCoroutine(FinishParticleShown(bulletParticle, bulletParticle.main.duration));
    }
    public void ActivateHitParticles(RaycastHit hit)
    {
        var impactParticle = _gunHitPool.GetFromPool();
        impactParticle.transform.position = hit.point;
        impactParticle.transform.rotation = Quaternion.identity;
        _coroutineObject.StartCoroutine(FinishParticleShown(impactParticle, impactParticle.main.duration));
    }

    public IEnumerator FinishParticleShown(ParticleSystem particle, float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        particle.Stop();
        particle.gameObject.SetActive(false);
    }

    public void ActivateTrailParticles(Vector3 hitPosition)
    {
        _coroutineObject.StartCoroutine(StartFrowTrail(hitPosition));
    }

    private IEnumerator StartFrowTrail(Vector3 hitPosition)
    {
        Vector3 startPoint = ShootParticle.transform.position + transform.TransformDirection(Vector3.forward);
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

