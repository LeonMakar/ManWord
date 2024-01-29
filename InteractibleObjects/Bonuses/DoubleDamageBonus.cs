using System.Collections;
using UnityEngine;

public class DoubleDamageBonus : InteractibleObjects
{
    [SerializeField] private float _bonusDuration;
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
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
        gun.SetGunDamage(gun.GunDamage * 2);
        yield return new WaitForSeconds(duration);
        gun.SetGunDamage(gun.GunDamage / 2);
        _meshRenderer.enabled = true;
        ParentObject.gameObject.SetActive(false);
    }
}
