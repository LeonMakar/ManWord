using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.PlayerConnection;
using Zenject;

public class MainPlayerController : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform _turgetTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField, Range(0f, 30f)] private float _speed;
    [SerializeField, Range(0f, 100f)] private float _sensetive;
    [SerializeField] private float aimingTime;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _rigAnimator;
    [SerializeField] private PlayerStateMachine _playerStateMachine;

    public CinemachineVirtualCamera AimCamera;
    public Transform AimCamTransform;


    private CharacterActions _inputActions;
    private Gun _gun;
    public UIMoneyShower Money {  get; private set; }
    private int _isMooving;
    private int _mooveValue;
    private int _shootTrigger;
    private int _reloadingTrigger;

    public Gun Gun => _gun;


    [Inject]
    private void Construct(CharacterActions inputActions, Gun gun,UIMoneyShower money)
    {
        _inputActions = inputActions;
        _gun = gun;
        Money = money;

        _inputActions.Default.Enable();
        _inputActions.Default.Aiming.performed += StartAiming;

        _shootTrigger = Animator.StringToHash("Shoot");
        _reloadingTrigger = Animator.StringToHash("Reload");
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        _playerStateMachine.InitStateMachine(_inputActions, _gun, _rigAnimator, _animator, _characterController, _speed);
    }

    public void AnimateShoot() => _animator.SetTrigger(_shootTrigger);
    public void AnimateReload() => _animator.SetTrigger(_reloadingTrigger);
    public void SetAnimatorControllerForGun(AnimatorController controller) => _animator.runtimeAnimatorController = controller;

    private void StartAiming(InputAction.CallbackContext context)
    {
        AimCamera.Priority = 15;
        StartCoroutine(StartAimingTimer(aimingTime));
        Time.timeScale = 0.5f;
    }
    public void StartAiming(float aimingBonusTime)
    {
        AimCamera.Priority = 15;
        StartCoroutine(StartAimingTimer(aimingBonusTime));
        Time.timeScale = 0.5f;

    }

    private IEnumerator StartAimingTimer(float aimingTime)
    {

        var saveSpred = _gun.GunData.GunSpred;
        _gun.GunData.GunSpred = Vector2.zero;
        yield return new WaitForSeconds(aimingTime);
        AimCamera.Priority = 9;
        Time.timeScale = 1;
        _gun.GunData.GunSpred = saveSpred;
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
