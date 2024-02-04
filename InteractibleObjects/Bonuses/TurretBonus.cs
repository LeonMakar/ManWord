using System.Collections;
using UnityEngine;

public class TurretBonus : InteractibleObjects
{
    [SerializeField] private float _bonusDuration;
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private MeshRenderer[] _meshRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            other.transform.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
                StartCoroutine(TimingBonusDuration(_bonusDuration));
            foreach (var renderer in _meshRenderer)
                renderer.enabled = false;
        }
    }
    public override void Update()
    {
        base.Update();
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
    private IEnumerator TimingBonusDuration(float duration)
    {
        GameConstans.ActivateTurret.Invoke(true);
        var currentspeed = Speed;
        Speed = 0;
        yield return new WaitForSeconds(duration);
        GameConstans.ActivateTurret.Invoke(false);
        foreach (var renderer in _meshRenderer)
            renderer.enabled = true;
        Speed = currentspeed;
        ParentObject.gameObject.SetActive(false);

    }
}
