using UnityEngine;

public class SimpleObstacle : InteractibleObjects
{
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PlayerTag)
        {
            other.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
                controller.GameStateMachine.TransitToGameOverState();
            gameObject.SetActive(false);
        }
    }
}
