using System.Collections;
using UnityEngine;

public class SwiftAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _currentPosition;
    [SerializeField] private RectTransform _positionToSwift;
    private Vector3 _startPos;
    [SerializeField] private float _swiftSpeed;

    private void Start()
    {
        WeaponChooser.Swipe += StartSwiftAnimation;
    }

    public void StartSwiftAnimation()
    {
        _startPos = _currentPosition.position;
        if (gameObject.activeSelf)
            StartCoroutine(Swifting());
    }


    private IEnumerator Swifting()
    {
        float _time = 0;
        while (_time <= 1.2f)
        {
            _currentPosition.position = Vector3.Lerp(_startPos, _positionToSwift.position, _time);
            _time += Time.deltaTime * _swiftSpeed;
            yield return null;
        }
        _time = 0;
        while (_time <= 1.2f)
        {
            _currentPosition.position = Vector3.Lerp(_positionToSwift.position, _startPos, _time);
            _time += Time.deltaTime * _swiftSpeed;
            yield return null;
        }
    }
}
