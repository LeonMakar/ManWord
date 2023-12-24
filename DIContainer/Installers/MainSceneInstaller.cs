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
    [SerializeField] private StartShoot _startShoot;


    public override void InstallBindings()
    {
        BindInputAction();
        BindEventBus();
        BindPlayer();
        BindGun();
        BindGunTrailRenderer();
        BindHealthBar();
        BindStartShoot();
        BindEnemyPool();
        BindEnemyFactory();

    }

    private void BindEnemyFactory()
    {
        Container
            .Bind<IEnemyFactory>()
            .To<EnemyFactory>()
            .AsSingle();
    }

    private void BindStartShoot()
    {
        Container.Bind<StartShoot>().FromInstance(_startShoot).AsSingle().NonLazy();
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
