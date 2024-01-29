using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBonus : InteractibleObjects, IShootable
{
    [SerializeField] private float _radiusOfExpolosian;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private ParticleSystem _expolosionParticles;
    [SerializeField] private GameObject _objectToRotate;
    private bool _isExplosed;

    public void Attacked(int damageValue)
    {
        if (!_isExplosed)
        {
            var enemys = Physics.OverlapSphere(transform.position, _radiusOfExpolosian);
            _isExplosed = true;
            foreach (var collider in enemys)
            {
                if (collider.tag == GameConstans.EnemyTag)
                {
                    collider.TryGetComponent(out Zombie zombie);
                    zombie?.DiactivatingRagdoll(false);
                    Vector3 Direction = (collider.transform.position + Vector3.up);/*- new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 100, gameObject.transform.position.z)).normalized*/
                    zombie?.AddForceToBody(Vector3.up * 1000);
                }
            }
            var particles = Instantiate(_expolosionParticles, transform.position, Quaternion.LookRotation(Vector3.up)); ;
            StartCoroutine(WaitBeforeDestroyParticles(2, particles.gameObject));
        }
    }

    public override void Update()
    {
        base.Update();
        _objectToRotate.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
    private IEnumerator WaitBeforeDestroyParticles(float duration, GameObject particles)
    {
        _objectToRotate.SetActive(false);
        yield return new WaitForSeconds(duration);
        Destroy(particles);
        _isExplosed = false;
        gameObject.SetActive(false);
    }
}
