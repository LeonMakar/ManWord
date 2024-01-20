using Cinemachine;
using System.Collections;
using UnityEngine;
using Zenject;

public class MenuChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraStart;
    [SerializeField] private CinemachineVirtualCamera _cameraFinish;
    [SerializeField] private Canvas _canvasToDisable;
    [SerializeField] private Canvas _canvasToEnable;
    private EventBus _eventBus;
    private float _cameraTransitionDuration = 2f;
    private GameObject _playerGameObject;

    [Inject]
    public void Construct(EventBus eventBus, GameObject playerGameObject)
    {
        _eventBus = eventBus;
        _playerGameObject = playerGameObject;
    }


    public void ChangeMenu()
    {
        _cameraStart.Priority--;
        _cameraFinish.Priority = 15;

        StartCoroutine(WaitCameraChangeTransition(_cameraTransitionDuration));
    }

    public void StartGame()
    {
        _cameraStart.Priority--;
        _cameraFinish.Priority = 15;
        _playerGameObject.SetActive(true);
        StartCoroutine(WaitCameraChangeTransition(_cameraTransitionDuration));

        _eventBus.Invoke(new StartGameSignal(true));
    }

    private IEnumerator WaitCameraChangeTransition(float transitionDuration)
    {
        if (_canvasToDisable != null)
            _canvasToDisable.enabled = false;
        yield return new WaitForSeconds(transitionDuration);
        if (_canvasToEnable != null)
            _canvasToEnable.enabled = true;
    }
}
