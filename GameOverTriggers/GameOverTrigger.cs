using UnityEngine;
using Zenject;

public class GameOverTrigger : MonoBehaviour
{
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _eventBus.Invoke<GameOverSignal>(new GameOverSignal(true));
            Debug.Log("ֲûחמג סמבûעטÿ");
        }
    }
}
