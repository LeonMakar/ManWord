using UnityEngine;
using Zenject;

public class GameOverTrigger : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameConstans.ENEMY_TAG)
        {
            _gameStateMachine.TransitToGameOverState();
        }
    }
}
