using System.Collections;
using UnityEngine;

public class GunTrail : MonoBehaviour, IService
{
    [SerializeField] TrailRenderer _trail;
    private Vector3 _shootPoint;
    public void SetShootPoint(Vector3 ShootDirection) => _shootPoint = ShootDirection;

    public IEnumerator FrowTrail()
    {
        float time = 0;
        TrailRenderer trailRenderer = Instantiate(_trail, _trail.transform.position, Quaternion.identity);
        trailRenderer.enabled = true;
        while (time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(_trail.transform.position, _shootPoint, time);
            time += Time.deltaTime / _trail.time;
            yield return null;
        }
        Destroy(trailRenderer.gameObject);
    }
}
