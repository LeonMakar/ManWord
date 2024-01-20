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
    [SerializeField] private int BaseMoneyToDrop;
    [SerializeField] private int MoneyRandomizingRange;

    [field: SerializeField] public float RotationInterval { get; private set; }

    protected string ZombieName;
    protected int HoldedMoney;

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

    private UIMoneyShower _moneyShower;
    private MainPlayerController _player;
    private Gun _gun;
    private HealthBar _healthBar;
    private bool _canChangeHealthBar = true;


    //----------Animator parameters fields----------//
    private int _isHited;
    private int _isDied;

    #region Initialization
    [Inject]
    public void Construct(HealthBar healthBar, UIMoneyShower moneyShower, MainPlayerController player, Gun gun)
    {
        _healthBar = healthBar;
        _moneyShower = moneyShower;
        _player = player;
        _gun = gun;
    }
    private void Awake()
    {
        _isHited = Animator.StringToHash("Hit");
        _isDied = Animator.StringToHash("isDead");
        CalculateMoneyHolding();
    }
    #endregion

    #region Ragdoll
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
    #endregion

    #region ActionsAfterDeath
    public void DoActionsAfterSpawning()
    {
        ChangeZombieScin();
        RandomizeZombieHealth();
        CalculateMoneyHolding();
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
    private void CalculateMoneyHolding()
    {
        HoldedMoney = Random.Range(BaseMoneyToDrop - MoneyRandomizingRange, BaseMoneyToDrop + MoneyRandomizingRange);
    }
    public void ResetAllParameters()
    {
        _skins[_scinNumber].SetActive(false);
        Health = MaxHealth;
        transform.position = Vector3.zero;
    }

    public abstract void DethActions();
    #endregion

    #region HealthBarConnection
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
    private IEnumerator CanChangeHealBarValues()
    {
        yield return new WaitForSeconds(2f);
        _canChangeHealthBar = true;
    }

    #endregion

    public void GetDamage(int damageValue)
    {
        if (Health - damageValue > 0)
            Health -= damageValue;
        else if (Health - damageValue <= 0 && Health != 0)
            Deading();

        _healthBar?.ChangeSliderValues(_maxHealthAfterRandomizing, 0, Health, damageValue);
        _animator.SetTrigger(_isHited);
    }
    private void Deading()
    {
        Health = 0;
        if (Time.timeScale < 1)
        {
            Debug.Log(_player);
            Debug.Log(_player.AimCamera);
            _player.AimCamera.Follow = _gun.GunParticles.TrailGameObject.transform;

        }
        _animator.SetTrigger(_isDied);
        _moneyShower.AddMoney(HoldedMoney);
    }
    public void StoppingZoomingBulletTrail()
    {
        _player.AimCamera.Follow = _player.AimCamTransform;
        //_player.AimCamera.Follow = null;
    }

    private void Update()
    {
        CharacterController.Move(Vector3.back * Speed * Time.deltaTime);
    }

}
