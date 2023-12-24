using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    [SerializeField] Gun _gun;
    [SerializeField] MainPlayerController _mainPlayerController;
    [SerializeField] GunTrail _gunTrail;
    [SerializeField] StartShoot _startShoot;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] EventBus _eventBus;
    [SerializeField] EnemyPool _enemyPool;
    [SerializeField] Spawner _spawner;
    

    private IContainer _mainContainer = new Container();
    private Injector _injector;
    private CharacterActions _inputActions;



    //private void Awake()
    //{
    //    _injector = new Injector(_mainContainer);
    //    Injector.Instance = _injector;
    //    _inputActions = new CharacterActions();
    //    //_enemyPool.Init();


    //    _injector.AddExistingSingletoneService<Gun, Gun>(_gun);
    //    _injector.AddExistingSingletoneService<MainPlayerController, MainPlayerController>(_mainPlayerController);
    //    _injector.AddExistingSingletoneService<CharacterActions, CharacterActions>(_inputActions);
    //    _injector.AddExistingSingletoneService<GunTrail, GunTrail>(_gunTrail);
    //    _injector.AddExistingSingletoneService<StartShoot, StartShoot>(_startShoot);
    //    _injector.AddExistingSingletoneService<HealthBar, HealthBar>(_healthBar);
    //    _injector.AddExistingSingletoneService<EventBus, EventBus>(_eventBus);
    //    _injector.AddExistingSingletoneService<EnemyPool, EnemyPool>(_enemyPool);
    //    _injector.AddExistingSingletoneService<Spawner, Spawner>(_spawner);

    //    //_injector.ScanInjectFields();
    //}
}
