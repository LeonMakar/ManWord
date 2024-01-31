using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _delayBeforExplosion;
    [SerializeField] private int _bombingDamage;
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioSource _explosionSound;

    private void Awake()
    {
        GameConstans.Bombarding += Bombing;
    }

    private void Bombing()
    {
        StartCoroutine(WaitToBombing());
    }
    private IEnumerator WaitToBombing()
    {
        yield return new WaitForSeconds(_delayBeforExplosion);
        var enemyColliders = Physics.OverlapSphere(transform.position, 6);
        var particle = Instantiate(_explosionParticle, transform.position, Quaternion.identity);
        _explosionSound.pitch = Random.Range(0.6f, 1f);
        _explosionSound.Play();
        StartCoroutine(WaitToDeleteParticle(particle.gameObject));
        foreach (var enemy in enemyColliders)
        {
            if (enemy.tag == GameConstans.EnemyTag)
            {
                enemy.TryGetComponent(out Zombie zombie);
                zombie?.DiactivatingRagdoll(false);
                zombie?.AddForceToBody(Vector3.up*850);
                zombie?.GetDamage(_bombingDamage);
            }
        }
    }

    private IEnumerator WaitToDeleteParticle(GameObject particleGameObject)
    {
        yield return new WaitForSeconds(3);
        Destroy(particleGameObject);
    }
}
