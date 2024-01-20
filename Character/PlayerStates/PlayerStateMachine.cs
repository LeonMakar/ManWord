using UnityEngine;


public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        Idle,
        Run
    }

    public void InitStateMachine(CharacterActions actionInput, Gun gun, Animator rigAnimator, Animator animator,
        CharacterController characterController, float speed)
    {
        MoovingState moovingState = new MoovingState(PlayerState.Run, actionInput, rigAnimator, animator, characterController, speed);
        IdleState idleState = new IdleState(PlayerState.Idle, actionInput,gun,animator);

        States.Add(PlayerState.Idle, idleState);
        States.Add(PlayerState.Run, moovingState);

        StartStateMachine(PlayerState.Run);
    }
}


