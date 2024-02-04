using TMPro;
using UnityEngine;
using Zenject;

public class GameStateMachine : StateManager<GameStateMachine.GameStates>
{
    [SerializeField] private Canvas _gamePlayCanvas;
    [SerializeField] private Canvas _gameMainMenuCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Spawner _enemySpawner;
    [SerializeField] private Spawner _bonusSpawner;
    [SerializeField] private Spawner _obstacleSpawner;


    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private GameObject[] _menuZombie;
    [SerializeField] private AudioSource _musicAudio;



    public enum GameStates
    {
        GameIsActive,
        GameIsOver,
        GameIsStoped,
    }

    [Inject]
    private void Cunstruct(EventBus eventBus, EnemyPool enemyPool, SaveAndLoadProcess saveAndLoad, UIMoneyShower uIMoneyShower, BonusePool bonusePool)
    {
        GameIsActiveState gameIsActiveState = new GameIsActiveState(GameStates.GameIsActive, _gamePlayCanvas, _enemySpawner,
            _playerGameObject, eventBus, _menuZombie, enemyPool, uIMoneyShower, _bonusSpawner, bonusePool, _obstacleSpawner,_musicAudio);

        GameIsOverState gameIsOverState = new GameIsOverState(GameStates.GameIsOver, _gameOverCanvas, saveAndLoad,uIMoneyShower);

        GameIsStopedState gameIsStopedState = new GameIsStopedState(GameStates.GameIsStoped, _gameMainMenuCanvas);

        States.Add(GameStates.GameIsActive, gameIsActiveState);
        States.Add(GameStates.GameIsOver, gameIsOverState);
        States.Add(GameStates.GameIsStoped, gameIsStopedState);

        StartStateMachine(GameStates.GameIsStoped);
    }

    public void TransitToGameActiveState() => TransitionToNextState(GameStates.GameIsActive);
    public void TransitToGameOverState() => TransitionToNextState(GameStates.GameIsOver);
    public void TransitToGameStopedState() => TransitionToNextState(GameStates.GameIsStoped);




}
