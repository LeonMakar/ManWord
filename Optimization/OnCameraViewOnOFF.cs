using UnityEngine;

public class OnCameraViewOnOFF : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Debug.Log("Не виден");
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }
}
