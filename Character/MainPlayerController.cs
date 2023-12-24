using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MainPlayerController : MonoBehaviour, IService, IInjectable
{
    [SerializeField] private Transform _turgetTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField, Range(0f, 30f)] private float _speed;
    [SerializeField, Range(0f, 100f)] private float _sensetive;
    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] private float aimingTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _rigAnimator;




    private CharacterActions _inputActions;
    private Gun _gun;
    private int _isMooving;
    private int _mooveValue;
    private int _shootTrigger;

    [Inject]
    private void Construct(CharacterActions inputActions, Gun gun)
    {
        _inputActions = inputActions;
        _gun = gun;

        _inputActions.Default.Enable();
        _inputActions.Default.Aiming.performed += Aiming;
        _isMooving = Animator.StringToHash("isMooving");
        _mooveValue = Animator.StringToHash("mooveValue");
        _shootTrigger = Animator.StringToHash("Shoot");
    }

    public void AnimateShoot()
    {
        _animator.SetTrigger(_shootTrigger);
    }

    public void IdleanimationStarted() => _gun.PlayerStopMooves();
    private void Aiming(InputAction.CallbackContext context)
    {
        _camera.Priority = 11;
        StartCoroutine(CloseAiming(aimingTime));
        Time.timeScale = 0.5f;
    }

    private IEnumerator CloseAiming(float aimingTime)
    {
        yield return new WaitForSeconds(aimingTime);
        _camera.Priority = 9;
        Time.timeScale = 1;


    }

    private void Mooving()
    {
        if (_inputActions.Default.Move.IsPressed())
        {
            _gun.PlayerMooves();
            _rigAnimator.enabled = false;
            _animator.SetBool(_isMooving, true);
            float input = _inputActions.Default.Move.ReadValue<float>() * Time.deltaTime * _speed;
            _animator.SetFloat(_mooveValue, input);
            _characterController.Move(new Vector3(input, 0, 0));
        }
        else
        {
            if (_animator.GetBool(_isMooving))
                _animator.SetBool(_isMooving, false);
            _rigAnimator.enabled = true;
        }
    }

    private void Rotation()
    {
        Vector2 input = _inputActions.Default.Rotate.ReadValue<Vector2>() * Time.deltaTime * _sensetive;
        if (input.x + _turgetTransform.position.x > 15)
            _turgetTransform.position = new Vector3(15, _turgetTransform.position.y, _turgetTransform.position.z);
        else if (input.x + _turgetTransform.position.x < -15)
            _turgetTransform.position = new Vector3(-15, _turgetTransform.position.y, _turgetTransform.position.z);
        else
            _turgetTransform.position = new Vector3(_turgetTransform.position.x + input.x, _turgetTransform.position.y, _turgetTransform.position.z);

        if (input.y + _turgetTransform.position.y > 10)
            _turgetTransform.position = new Vector3(_turgetTransform.position.x, 10, _turgetTransform.position.z);
        else if (input.y + _turgetTransform.position.y < -10)
            _turgetTransform.position = new Vector3(_turgetTransform.position.x, -10, _turgetTransform.position.z);
        else
            _turgetTransform.position = new Vector3(_turgetTransform.position.x, _turgetTransform.position.y + input.y, _turgetTransform.position.z);


    }
    private void Update()
    {
        Mooving();
        Rotation();
    }
}
