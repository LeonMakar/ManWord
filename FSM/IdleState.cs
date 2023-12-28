using System;
using UnityEngine;

public class IdleState : BaseState<PlayerStateMachine.PlayerState>
{
    private readonly CharacterActions ActionInput;
    private readonly Gun Gun;
    private readonly Animator Animator;
    private readonly int _shootTrigger;

    public IdleState(PlayerStateMachine.PlayerState key, CharacterActions actionInput, Gun gun,Animator animator) : base(key)
    {
        ActionInput = actionInput;
        Gun = gun;
        Animator = animator;

        _shootTrigger = Animator.StringToHash("Shoot");

    }

    public override void EnterToState()
    {
        Gun.PlayerStopMooves();
    }

    public override void ExitFromState()
    {
        
    }

    public override void UpdateState()
    {
        if(ActionInput.Default.Move.IsPressed()) 
        {
            ChangeStateAction.Invoke(PlayerStateMachine.PlayerState.Run);
        }
    }


}


