using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Gun : MonoBehaviour
{
    // -------------- Points ------------------//

    [SerializeField] private Transform _headPoint;
    [SerializeField] private Transform _gunPlace;
    [field: SerializeField] public Transform RawSpawnPoint { get; private set; }
    [field: SerializeField] public Transform RawEndPoint { get; private set; }
    [field: SerializeField] public Transform BulletAimPoint { get; private set; }


    // -------------- Gun perfomance ------------------//
    public float RateOfFire { get; private set; }
    public int GunDamage { get; private set; }
    public int BulletAmmount { get; set; }
    public int MaxBulletAmmount { get; private set; }
    public float ReloadingTime { get; private set; }

    [HideInInspector] public bool IsAiming = false;




    // -------------- Gun settings ------------------//

    [field: SerializeField] public LayerMask HitableLayer { get; private set; }
    [SerializeField] private int _arenaMaxDistance = 60;

    // -------------- Services ------------------//
    [field: SerializeField] public GunParticles GunParticles { get; private set; }
    [field: SerializeField] public AudioSource AudioSource { get; private set; }
    public GunData GunData { get; private set; }
    public MainPlayerController MainPlayerController { get; private set; }
    public GunTrail GunTrail { get; private set; }
    public CharacterActions CharacterActions { get; private set; }

    [SerializeField] private GunStateMachine _gunStateMachine;
    [field: SerializeField] public LineRenderer LineRenderer { get; private set; }

    private EventBus _eventBus;
    public bool IsGameStarted { get; private set; }
    public bool TargetIsFinded { get; private set; }

    private GameObject _gunPrefab;
    #region Initialization
    [Inject]
    private void Construct(MainPlayerController mainPlayerController, GunTrail gunTrail, EventBus eventBus, CharacterActions inputActions)
    {
        MainPlayerController = mainPlayerController;
        GunTrail = gunTrail;
        CharacterActions = inputActions;
        _eventBus = eventBus;
        _eventBus.Subscrube<StartGameSignal>(GameIsStarted);
        _gunStateMachine.InitStateMachine(this, CharacterActions);
    }

    private void SynchronizeWeaponeData()
    {
        RateOfFire = GunData.RateOfFire;
        GunDamage = GunData.Damage;
        MaxBulletAmmount = GunData.BulletAmmount;
        BulletAmmount = MaxBulletAmmount;
        ReloadingTime = GunData.ReloadingTime;

        CreateWeaponPrefab();
    }
    private void OnEnable()
    {
        _gunStateMachine.StartStateMachine();
    }
    private void InitializeGunParticles(GameObject weaponPrefab)
    {
        weaponPrefab.TryGetComponent(out GunParticlesPlace particlePlaces);
        if (particlePlaces != null)
        {
            GunParticles.BulletParticle = particlePlaces.BulletParticle;
            GunParticles.ShootParticle = particlePlaces.ShootParticle;
            GunParticles.InitializeGunParticlePools();
        }
        else
            throw new Exception("Script GunParticlePlace not found in Gun prefab");
    }

    private void CreateWeaponPrefab()
    {
        if (_gunPrefab != null)
            Destroy(_gunPrefab);
        _gunPrefab = Instantiate(GunData.GunPrefab, _gunPlace.position, _gunPlace.rotation);
        _gunPrefab.transform.parent = transform;
        _gunPrefab.transform.localScale = _gunPlace.localScale;
        InitializeGunParticles(_gunPrefab);
    }
    #endregion
    public void EqipeNewGun(GunData gun)
    {
        GunData = gun;
        MainPlayerController.SetAnimatorControllerForGun(GunData.AnimatorController);
        SynchronizeWeaponeData();
    }

    public void SetGunDamage(int damage) => GunDamage = damage;
    public void SetEquipedGun(GunData gunData) => GunData = gunData;
    private void GameIsStarted(StartGameSignal signal)
    {
        IsGameStarted = signal.GameIsStarted;
    }


    private void Update()
    {
        FindTargetForAimPoint();
    }


    #region AimPointLogic
    private void FindTargetForAimPoint()
    {
        Ray ray = new Ray(_headPoint.position, RawSpawnPoint.TransformDirection(Vector3.forward));
        if (Physics.Raycast(ray, out RaycastHit hit, Int32.MaxValue, LayerMask.GetMask("Default", "Hitable")))
        {
            if (hit.collider.transform.gameObject.layer == 6)
            {
                BulletAimPoint.GetComponentInChildren<Renderer>().material.color = Color.red;
                TargetIsFinded = true;
                hit.transform.TryGetComponent<ZombieShootPoint>(out ZombieShootPoint shootPoint);
                shootPoint?.InvokeOnUnderAimAction();
            }
            else
            {
                BulletAimPoint.GetComponentInChildren<Renderer>().material.color = Color.green;
                TargetIsFinded = false;
            }

            ResizeAimPoint(hit);
            LineRenderer.SetPosition(0, GunParticles.ShootParticle.transform.position);
            LineRenderer.SetPosition(1, BulletAimPoint.position);
        }
    }

    private void ResizeAimPoint(RaycastHit hit)
    {
        float distance = Vector3.Distance(RawSpawnPoint.position, hit.point);
        float scale = Mathf.Lerp(1, 4, distance / _arenaMaxDistance);
        BulletAimPoint.localScale = new Vector3(scale, scale, scale); ;
        BulletAimPoint.position = hit.point;
    }
    #endregion
}


