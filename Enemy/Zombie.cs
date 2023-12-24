using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Zombie : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    private int _changebleMaxHealth;
    [SerializeField] protected float Speed;
    [SerializeField] protected CharacterController CharacterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _skins;
    [SerializeField] private int _healthRandomizerNumber = 30;

    protected string ZombieName;
    protected GameObject ActiveScin;


    private int _isHited;
    private int _isDied;
    [Inject]
    private HealthBar _healthBar;
    private bool _canChangeHealthBar = true;

    [Inject]
    public void Construct(HealthBar healthBar)
    {
        _healthBar = healthBar;
    }
    private void Awake()
    {
        _isHited = Animator.StringToHash("Hit");
        _isDied = Animator.StringToHash("isDead");
    }
    private void OnEnable()
    {
        ChangeZombieScin();
        RandomizeZombieHealth();
    }

    private void ChangeZombieScin()
    {
        int scinNumber = UnityEngine.Random.Range(0, _skins.Count);
        _skins[scinNumber].SetActive(true);
        ActiveScin = _skins[scinNumber];
        ZombieName = ActiveScin.name;
    }

    private void RandomizeZombieHealth()
    {
        _changebleMaxHealth = UnityEngine.Random.Range(MaxHealth - _healthRandomizerNumber, MaxHealth + _healthRandomizerNumber);
        Health = _changebleMaxHealth;
    }

    public void GetDamage(int damageValue)
    {
        if (Health - damageValue > 0)
            Health -= damageValue;
        else if (Health > 0)
        {
            Health = 0;
            _animator.SetTrigger(_isDied);
        }

        _healthBar?.ChangeSliderValues(_changebleMaxHealth, 0, Health, damageValue);
        //_animator.SetLayerWeight(1, 1);
        _animator.SetTrigger(_isHited);
    }

    public void ResetAllParameters()
    {
        Health = MaxHealth;
        transform.position = Vector3.zero;
    }

    public abstract void DethActions();

    public void OnUnderAim()
    {
        if (_healthBar.IsHealthBarInvisible())
        {
            if (!_healthBar.EnemyOnUnderAim)
                StartCoroutine(_healthBar?.SetOnHelthBar());
        }

        if (_canChangeHealthBar)
        {
            _canChangeHealthBar = false;
            _healthBar.EnemyOnUnderAim = true;
            _healthBar.SetNewHealthBarValue(ZombieName, _changebleMaxHealth, Health);
            StartCoroutine(CanChangeHealBarValues());
        }

    }

    private void Update()
    {
        CharacterController.Move(Vector3.back * Speed * Time.deltaTime);
    }

    private IEnumerator CanChangeHealBarValues()
    {
        yield return new WaitForSeconds(2f);
        _canChangeHealthBar = true;
    }


}
public struct ZombieProperties
{
    public string Name;
    public float MaxHealth;
    public float CurrentHEalth;

    public ZombieProperties(string name, float maxHealth, float currentHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHEalth = currentHealth;
    }
}
