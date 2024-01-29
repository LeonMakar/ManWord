using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private Gun _gun;
    [SerializeField] private MainPlayerController _player;
    [SerializeField] private GunTrail _gunTrail;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private BonusePool _bonusPool;
    [SerializeField] private WeaponSaveData _weaponSaveData;
    [SerializeField] private UIMoneyShower _moneyShower;
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private SaveAndLoadProcess _saveAndLoadProcess;

    public override void InstallBindings()
    {
        BindInputAction();
        BindEventBus();
        BindPlayer();
        BindPlayerGameObject();
        BindGun();
        BindGunTrailRenderer();
        BindHealthBar();
        BindEnemyPool();
        BindBonusPool();
        BindEnemyFactory();
        BindWeaponSaveData();
        BindMoneyShower();
        BindGameStateMachine();
        BindSaveAndLoadPrecess();
    }

    private void BindBonusPool()
    {
        Container
            .Bind<BonusePool>()
            .FromInstance(_bonusPool)
            .AsSingle()
            .NonLazy();
    }

    private void BindSaveAndLoadPrecess()
    {
        Container
            .Bind<SaveAndLoadProcess>()
            .FromInstance(_saveAndLoadProcess)
            .AsSingle()
            .NonLazy();
    }

    private void BindGameStateMachine()
    {
        Container
            .Bind<GameStateMachine>()
            .FromInstance(_gameStateMachine)
            .AsSingle()
            .NonLazy();
    }
    private void BindMoneyShower()
    {
        Container
            .Bind<UIMoneyShower>()
            .FromInstance(_moneyShower)
            .AsSingle();
    }
    private void BindWeaponSaveData()
    {
        Container
            .Bind<WeaponSaveData>()
            .FromInstance(_weaponSaveData)
            .AsSingle();
    }
    private void BindPlayerGameObject()
    {
        Container
                    .Bind<GameObject>()
                    .FromInstance(_player.gameObject)
                    .AsSingle();
    }
    private void BindEnemyFactory()
    {
        Container
            .Bind<IFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }
    private void BindEnemyPool()
    {
        Container.Bind<EnemyPool>().FromInstance(_enemyPool).AsSingle().NonLazy();
    }
    private void BindHealthBar()
    {
        Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle().NonLazy();
    }
    private void BindGunTrailRenderer()
    {
        Container.Bind<GunTrail>().FromInstance(_gunTrail).AsSingle();
    }
    private void BindPlayer()
    {
        Container.Bind<MainPlayerController>()
                    .FromInstance(_player)
                    .AsSingle().NonLazy();
    }
    private void BindEventBus()
    {
        Container.Bind<EventBus>().AsSingle().NonLazy();
    }
    private void BindInputAction()
    {
        Container.Bind<CharacterActions>().FromNew()
            .AsSingle().NonLazy();

    }
    private void BindGun()
    {
        Container.Bind<Gun>()
            .FromInstance(_gun).AsSingle();
    }
    public void Initialize()
    {

    }
}
