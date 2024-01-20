using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimingBonus : InteractibleObjects
{
    [SerializeField] private GameObject _mehedObject;
    [SerializeField] private float _bonusDuration;
    [SerializeField] private MeshRenderer _meshRenderer;
    public override void Update()
    {
        base.Update();
        _mehedObject.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.TryGetComponent(out MainPlayerController controller);
        if (controller != null)
        {
            StartCoroutine(TimingBonusDuration(_bonusDuration, controller));
            _mehedObject.TryGetComponent(out MeshRenderer renderer);
            _meshRenderer.enabled = false;
        }
    }
    private IEnumerator TimingBonusDuration(float duration, MainPlayerController mainPlayerController)
    {
        mainPlayerController.StartAiming(duration);
        yield return new WaitForSeconds(duration);
        Destroy(ParentObject.gameObject);
    }
}
