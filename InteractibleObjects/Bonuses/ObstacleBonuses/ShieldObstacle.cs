using UnityEngine;

public class ShieldObstacle : InteractibleObjects
{
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == GameConstans.PLAYER_TAG)
        {
            other.TryGetComponent(out MainPlayerController controller);
            if (controller != null)
                controller.GameStateMachine.TransitToGameOverState();
            gameObject.SetActive(false);
        }
    }
}
