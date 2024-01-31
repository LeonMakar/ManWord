using System.Collections;
using UnityEngine;

public class ExplosiveBonus : InteractibleObjects, IShootable
{
    [SerializeField] private float _radiusOfExpolosian;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private ParticleSystem _expolosionParticles;
    [SerializeField] private GameObject _objectToRotate;
    [SerializeField] private int _damageValue;
    [SerializeField] private AudioSource _explosionAudio;
    public void Attacked(int damageValue)
    {
        CreatOverlapSphereToDetectEnemy();
        var particles = Instantiate(_expolosionParticles, transform.position, Quaternion.LookRotation(Vector3.up));
        StartCoroutine(WaitBeforeDestroyParticles(2, particles.gameObject));
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        PlaySound();

    }

    private void PlaySound()
    {
        _explosionAudio.pitch = Random.Range(0.5f, 1f);
        _explosionAudio.Play();
    }

    private void CreatOverlapSphereToDetectEnemy()
    {
        var enemys = Physics.OverlapSphere(transform.position, _radiusOfExpolosian);
        foreach (var collider in enemys)
        {
            if (collider.tag == GameConstans.EnemyTag)
            {
                collider.TryGetComponent(out Zombie zombie);
                zombie?.DiactivatingRagdoll(false);
                Vector3 Direction = (collider.transform.position + Vector3.up);
                zombie?.AddForceToBody(Vector3.up * 1000);
                zombie.GetDamage(_damageValue);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        _objectToRotate.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
    private IEnumerator WaitBeforeDestroyParticles(float duration, GameObject particles)
    {
        yield return new WaitForSeconds(duration);
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.SetActive(false);

        Destroy(particles);
    }
}
