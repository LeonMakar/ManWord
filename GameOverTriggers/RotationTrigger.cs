using System.Collections;
using UnityEngine;

public class RotationTrigger : MonoBehaviour
{
    [SerializeField] private Transform _lookRotationTarget;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.transform.TryGetComponent(out Zombie zombie);
            zombie.StartCoroutine(RotateToTarget(other.transform, zombie));
        }
    }

    private IEnumerator RotateToTarget(Transform objectTransform, Zombie zombieComponent)
    {
        float angle = CalculateAngle(objectTransform, out Vector3 directionToTarget);
        float time = 0;
        Quaternion rotation = Quaternion.LookRotation(directionToTarget);

        while (time < 1)
        {
            objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, rotation, time);
            time += 0.001f;
            if (!objectTransform.gameObject.activeSelf)
                zombieComponent.StopAllCoroutines();
            yield return new WaitForSeconds(zombieComponent.RotationInterval);
        }
    }

    private float CalculateAngle(Transform objectTransform, out Vector3 directionToTarget)
    {
        directionToTarget = _lookRotationTarget.position - objectTransform.position;
        directionToTarget.y = 0;
        float angle = Vector3.Angle(objectTransform.position, directionToTarget);
        return angle;
    }
}
