using UnityEngine;

public class OnCameraViewOnOFF : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Debug.Log("�� �����");
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }
}
