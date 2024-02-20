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

    protected int HoldedMoney;

    private int _maxHealthAfterRandomizing;
    public bool IsStartDying;
    public int GetHealthOfZombie() => Health;
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
    [SerializeField] private TranslationZombieName ZombieName;


    protected GameObject ActiveScin;

    private UIMoneyShower _moneyShower;
    private MainPlayerController _player;
    private Gun _gun;
    private HealthBar _healthBar;
    private bool _canChangeHealthBar = true;


    //----------Animator parameters fields----------//
    private int _isHited;
    private int _isDied;
    private int _isStandUp;
    private bool _isCrowling;
    public bool CanDisableHealthBar;
    private SkinnedMeshRenderer _meshRenderer;
    private MaterialPropertyBlock _materialPropertuBlock;

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
        _isStandUp = Animator.StringToHash("StandUp");
        CalculateMoneyHolding();
        _materialPropertuBlock = new MaterialPropertyBlock();

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
        _isCrowling = boolian;
        _animator.enabled = boolian;
        foreach (var rigidbody in _bodies)
        {
            rigidbody.isKinematic = boolian;
        }
        StartCoroutine(StandUp());
    }

    private IEnumerator StandUp()
    {
        yield return new WaitForSeconds(5);
        _animator.enabled = true;
        foreach (var rigidbody in _bodies)
        {
            rigidbody.isKinematic = true;
        }
        if (Health > 0)
            _animator.SetTrigger(_isStandUp);
        else
        {
            Health = 0;
            _moneyShower.ChangeMoneyValue(HoldedMoney);
            DethActions();
            IsStartDying = true;
        }
    }
    #endregion

    #region ActionsAfterDeath
    public void DoActionsAfterSpawning()
    {
        ChangeZombieScin();
        RandomizeZombieHealth();
        CalculateMoneyHolding();
        IsStartDying = false;
        gameObject.layer = 6;

    }
    private void ChangeZombieScin()
    {
        _scinNumber = UnityEngine.Random.Range(0, _skins.Count);
        _skins[_scinNumber].SetActive(true);
        ActiveScin = _skins[_scinNumber];
        _meshRenderer = ActiveScin.transform.GetComponent<SkinnedMeshRenderer>();
        _meshRenderer.SetPropertyBlock(_materialPropertuBlock);
    }

    private void RandomizeZombieHealth()
    {
        _maxHealthAfterRandomizing = UnityEngine.Random.Range(MaxHealth - _healthRandomizerNumber, MaxHealth + _healthRandomizerNumber);
        Health = _maxHealthAfterRandomizing;
    }
    private void CalculateMoneyHolding()
    {
        HoldedMoney = UnityEngine.Random.Range(BaseMoneyToDrop - MoneyRandomizingRange, BaseMoneyToDrop + MoneyRandomizingRange);
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
        _healthBar.SetNewHealthBarValue(ZombieName.ZombieName, _maxHealthAfterRandomizing, Health);
    }
    #endregion


    public void GetDamage(int damageValue)
    {
        if (Health - damageValue > 0)
            Health -= damageValue;
        else if (Health - damageValue <= 0 && Health != 0)
        {
            Deading();
            IsStartDying = true;
        }

        _healthBar?.ChangeSliderValues(_maxHealthAfterRandomizing, 0, Health, damageValue);
        _animator.SetTrigger(_isHited);
    }
    private void Deading()
    {
        Health = 0;
        gameObject.layer = 1;
        if (!_isCrowling)
            _animator.SetTrigger(_isDied);
        else
            DethActions();

        _moneyShower.ChangeMoneyValue(HoldedMoney);
        _isCrowling = false;
        GameConstans.DiffcultyValue++;
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
