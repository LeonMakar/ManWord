using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField] private Transform _gunTransform;
    private CharacterActions _inputActions;
    [SerializeField] private CharacterController _characterController;
    [SerializeField, Range(0f, 30f)] private float _speed;
    [SerializeField, Range(0f, 100f)] private float _sensetive;
    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] private float aimingTime;
    [SerializeField] private Animator _animator;


    private int _isMooving;
    private int _mooveValue;

    private void Awake()
    {
        _inputActions = new CharacterActions();
        _inputActions.Default.Enable();
        _inputActions.Default.Aiming.performed += Aiming;
        _isMooving = Animator.StringToHash("isMooving");
        _mooveValue = Animator.StringToHash("mooveValue");
    }

    private void Aiming(InputAction.CallbackContext context)
    {
        _camera.Priority = 11;
        StartCoroutine(CloseAiming(aimingTime));
    }

    private IEnumerator CloseAiming(float aimingTime)
    {
        yield return new WaitForSeconds(aimingTime);
        _camera.Priority = 9;

    }

    private void Mooving()
    {
        if (_inputActions.Default.Move.IsPressed())
        {
            //_animator.SetBool(_isMooving, true);
            float input = _inputActions.Default.Move.ReadValue<float>() * Time.deltaTime * _speed;
            //_animator.SetFloat(_mooveValue, input);
            _characterController.Move(new Vector3(input, 0, 0));
        }
        else
        {
            //_animator.SetBool(_isMooving, false);
        }
    }

    private void Rotation()
    {
        Vector2 input = _inputActions.Default.Rotate.ReadValue<Vector2>() * Time.deltaTime * _sensetive;
        transform.Rotate(Vector3.up, input.x);
        _gunTransform.Rotate(Vector3.right, -input.y);
        Debug.Log(_gunTransform.rotation);
    }
    private void Update()
    {
        Mooving();
        Rotation();
    }
}
