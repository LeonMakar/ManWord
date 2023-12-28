using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField] private Transform _turgetTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField, Range(0f, 30f)] private float _speed;
    [SerializeField, Range(0f, 100f)] private float _sensetive;
    [SerializeField] CinemachineVirtualCamera _camera;
    [SerializeField] private float aimingTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _rigAnimator;
    [SerializeField] private PlayerStateMachine _playerStateMachine;



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

        _shootTrigger = Animator.StringToHash("Shoot");

        InitStateMachine();
    }

    private void InitStateMachine()
    {

        _playerStateMachine.InitStateMachine(_inputActions, _gun,_rigAnimator,_animator,_characterController,_speed);
    }

    public void AnimateShoot()
    {
        _animator.SetTrigger(_shootTrigger);
    }

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
        Rotation();
    }
}
