using UnityEngine;


public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    private CharacterActions actionInput;
    private Gun gun;
    private Animator rigAnimator;
    private Animator animator;
    private CharacterController characterController;
    private float speed;

    public enum PlayerState
    {
        Idle,
        Run
    }

    public void InitStateMachine(CharacterActions actionInput, Gun gun, Animator rigAnimator, Animator animator,
        CharacterController characterController, float speed)
    {
        MoovingState moovingState = new MoovingState(PlayerState.Run, actionInput, gun, rigAnimator, animator, characterController, speed);
        IdleState idleState = new IdleState(PlayerState.Idle, actionInput,gun,animator);

        this.actionInput = actionInput;
        this.gun = gun;
        this.rigAnimator = rigAnimator;
        this.animator = animator;
        this.characterController = characterController;
        this.speed = speed;


        States.Add(PlayerState.Idle, idleState);
        States.Add(PlayerState.Run, moovingState);

        StartStateMachine(PlayerState.Run);
    }
}


