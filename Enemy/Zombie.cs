using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Zombie : MonoBehaviour
{
    //----------Zombie characteristics----------//
    [Space(10), Header("Characteristics")]
    [SerializeField] protected int Health;
    [SerializeField] protected int MaxHealth;
    [SerializeField] protected float Speed;
    [SerializeField] private int _healthRandomizerNumber = 30;
    [field: SerializeField] public float RotationInterval { get; private set; }

    protected string ZombieName;

    private int _maxHealthAfterRandomizing;

    //----------Scins----------//
    [Space(10), Header("Scins and RididBodyes")]

    [SerializeField] private Rigidbody _rigidBodyCenter;
    [SerializeField] private List<GameObject> _skins;
    [SerializeField] private List<Rigidbody> _bodies;
    private int _scinNumber;

    //----------Services----------//
    [Space(10), Header("Services")]
    [SerializeField] private Animator _animator;
    [SerializeField] protected CharacterController CharacterController;

    protected GameObject ActiveScin;

    private HealthBar _healthBar;
    private bool _canChangeHealthBar = true;


    //----------Animator parameters fields----------//
    private int _isHited;
    private int _isDied;

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
    public void AddForceToBody(Vector3 forceDirection)
    {
        _rigidBodyCenter.AddForce(forceDirection, ForceMode.Impulse);
    }

    /// <summary>
    /// True - Ragdoll is Unactive,
    /// False - Ragdoll is Active
    /// </summary>
    /// <param name="boolian"></param>
    public void DiactivatingRagdoll(bool boolian)
    {
        _animator.enabled = boolian;
        foreach (var rigidbody in _bodies)
        {
            rigidbody.isKinematic = boolian;
        }
    }
    public void DoActionsAfterSpawning()
    {
        ChangeZombieScin();
        RandomizeZombieHealth();
    }
    private void ChangeZombieScin()
    {
        _scinNumber = UnityEngine.Random.Range(0, _skins.Count);
        _skins[_scinNumber].SetActive(true);
        ActiveScin = _skins[_scinNumber];
        ZombieName = ActiveScin.name;
    }

    private void RandomizeZombieHealth()
    {
        _maxHealthAfterRandomizing = UnityEngine.Random.Range(MaxHealth - _healthRandomizerNumber, MaxHealth + _healthRandomizerNumber);
        Health = _maxHealthAfterRandomizing;
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

        _healthBar?.ChangeSliderValues(_maxHealthAfterRandomizing, 0, Health, damageValue);
        //_animator.SetLayerWeight(1, 1);
        _animator.SetTrigger(_isHited);
    }

    public void ResetAllParameters()
    {
        _skins[_scinNumber].SetActive(false);
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
            _healthBar.SetNewHealthBarValue(ZombieName, _maxHealthAfterRandomizing, Health);
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
