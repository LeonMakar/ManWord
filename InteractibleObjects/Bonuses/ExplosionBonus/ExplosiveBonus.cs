using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBonus : InteractibleObjects, IShootable
{
    [SerializeField] private AreaOfAction _areaOfAction;
    [SerializeField] private int _rotationSpeed;
    [SerializeField] private ParticleSystem _expolosionParticles;

    public void Attacked(int damageValue)
    {
        Debug.Log("Взрыв ");
        foreach (var gameObject in _areaOfAction.GetAllObjectsInArea())
        {
            gameObject.TryGetComponent(out Zombie zombie);
            zombie.DiactivatingRagdoll(false);
            Vector3 Direction = (gameObject.transform.position + Vector3.up);/*- new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 100, gameObject.transform.position.z)).normalized*/
            zombie.AddForceToBody(Vector3.up * 1000);
            Debug.Log(Direction);
        }
        var particles = Instantiate(_expolosionParticles, transform.position, Quaternion.LookRotation(Vector3.up)); ;
        StartCoroutine(WaitBeforeDestroyParticles(2, particles.gameObject));
        Destroy(gameObject);
    }

    public override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.down, _rotationSpeed * Time.deltaTime);
    }
    private IEnumerator WaitBeforeDestroyParticles(float duration, GameObject particles)
    {
        yield return new WaitForSeconds(duration);
        Destroy(particles);
    }
}
