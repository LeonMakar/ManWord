using System.Collections;
using UnityEngine;

public class DoubleDamageBonus : InteractibleObjects
{
    [SerializeField] private float _bonusDuration;
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PLAYER_TAG)
        {
            other.transform.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
                StartCoroutine(TimingBonusDuration(_bonusDuration, controller.Gun));

            _meshedObject.gameObject.TryGetComponent(out MeshRenderer mesh);
            _meshRenderer.enabled = false;
        }
    }
    public override void Update()
    {
        base.Update();
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
    private IEnumerator TimingBonusDuration(float duration, Gun gun)
    {
        if (!gun.IsDamageBusted)
        {
            gun.SetGunDamage(gun.GunDamage * 2);
            var currentspeed = Speed;
            Speed = 0;
            yield return new WaitForSeconds(duration);
            gun.SetGunDamage(gun.GunDamage / 2);
            Speed = currentspeed;
            gun.IsDamageBusted = false;
            _meshRenderer.enabled = true;
            ParentObject.gameObject.SetActive(false);
        }
    }
}
