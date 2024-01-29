using System.Collections;
using UnityEngine;

public class AimingBonus : InteractibleObjects
{
    [SerializeField] private GameObject _meshedObject;
    [SerializeField] private float _bonusDuration;
    [SerializeField] private MeshRenderer _meshRenderer;
    public override void Update()
    {
        base.Update();
        _meshedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            other.transform.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
            {
                StartCoroutine(TimingBonusDuration(_bonusDuration, controller));
                _meshedObject.TryGetComponent(out MeshRenderer renderer);
                _meshRenderer.enabled = false;
            }
        }
    }
    private IEnumerator TimingBonusDuration(float duration, MainPlayerController mainPlayerController)
    {
        mainPlayerController.StartAiming(duration);
        yield return new WaitForSeconds(duration);
        _meshRenderer.enabled = true;
        ParentObject.gameObject.SetActive(false);
    }
}
