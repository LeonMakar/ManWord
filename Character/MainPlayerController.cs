using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField] private Transform _turgetTransform;
    [SerializeField] private CharacterController _characterController;
    [SerializeField, Range(0f, 30f)] private float _speed;
    [SerializeField, Range(0f, 100f)] private float _sensetive;
    [SerializeField] private float aimingTime;
    [SerializeField] private GameObject _animatorGameObject;
    [SerializeField] private Animator _rigAnimator;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private Canvas _mobileCanvas;

    public CinemachineVirtualCamera AimCamera;
    public Transform AimCamTransform;
    [SerializeField] private Animator _animator;
    public GameStateMachine GameStateMachine { get; private set; }

    private CharacterActions _inputActions;
    private Gun _gun;
    public UIMoneyShower Money { get; private set; }
    private int _isMooving;
    private int _mooveValue;
    private int _shootTrigger;
    private int _reloadingTrigger;
    private float _aimingCooldown = 5;
    private bool _canAiming = true;
    private float saveSpred;

    public Gun Gun => _gun;


    [Inject]
    private void Construct(CharacterActions inputActions, Gun gun, UIMoneyShower money, GameStateMachine gameStateMachine)
    {
        _inputActions = inputActions;
        _gun = gun;
        Money = money;
        GameStateMachine = gameStateMachine;

        _inputActions.Default.Enable();
        _inputActions.Default.Aiming.performed += StartAiming;

    }
    private void OnEnable()
    {
        if (GameConstans.IsMobile)
        {
            _mobileCanvas.enabled = true;
        }
    }
    private void OnDisable()
    {
        _mobileCanvas.enabled = false;

    }
    private void Start()
    {
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
    public void SetAnimatorControllerForGun(Animator animator)
    {

        _animator.runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    private IEnumerator CooldawnAiming()
    {
        yield return new WaitForSeconds(_aimingCooldown);
        _canAiming = true;
    }
    private void StartAiming(InputAction.CallbackContext context)
    {
        if (_canAiming)
        {
            AimCamera.Priority = 15;
            StartCoroutine(StartAimingTimer(aimingTime));
            Time.timeScale = 0.5f;
            _canAiming = false;
        }
    }
    public void StartAiming(float aimingBonusTime)
    {
        AimCamera.Priority = 15;
        StartCoroutine(StartAimingTimer(aimingBonusTime));
        Time.timeScale = 0.5f;

    }

    private IEnumerator StartAimingTimer(float aimingTime)
    {

        saveSpred = _gun.GunData.GunSpred;
        _gun.GunData.GunSpred = 0;
        yield return new WaitForSeconds(aimingTime);
        AimCamera.Priority = 9;
        Time.timeScale = 1;
        _gun.GunData.GunSpred = saveSpred;
        StartCoroutine(CooldawnAiming());

    }
    public void ResetSpredAndAim()
    {
        _gun.GunData.GunSpred = saveSpred;
        if (Time.timeScale < 1)
        {
            AimCamera.Priority = 9;
            Time.timeScale = 1;
            _canAiming = true;
        }
    }
    private void Rotation()
    {
        if (GameConstans.IsMobile)
            _sensetive = 2.5f;
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
