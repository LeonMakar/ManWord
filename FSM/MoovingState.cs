using UnityEngine;

public class MoovingState : BaseState<PlayerStateMachine.PlayerState>
{
    private CharacterActions _inputActions;
    private Gun _gun;
    private Animator _rigAnimator;
    private Animator _animator;
    private CharacterController _characterController;
    private float _speed;
    private int _isMooving;
    private int _mooveValue;
    public MoovingState(PlayerStateMachine.PlayerState key, CharacterActions actionInput, Gun gun, Animator rigAnimator, Animator animator,
        CharacterController characterController, float speed) : base(key)
    {
        _inputActions = actionInput;
        _gun = gun;
        _rigAnimator = rigAnimator;
        _animator = animator;
        _characterController = characterController;
        _speed = speed;

        _isMooving = Animator.StringToHash("isMooving");
        _mooveValue = Animator.StringToHash("mooveValue");
    }

    public override void EnterToState()
    {
        _gun.PlayerMooves();
        _rigAnimator.enabled = false;
        _animator.SetBool(_isMooving, true);
    }

    public override void ExitFromState()
    {
        if (_animator.GetBool(_isMooving))
            _animator.SetBool(_isMooving, false);
        _rigAnimator.enabled = true;
    }

    public override void UpdateState()
    {
        if (!_inputActions.Default.Move.IsPressed())
        {
            ChangeStateAction.Invoke(PlayerStateMachine.PlayerState.Idle);
            return;
        }
        Moove();
    }

    public void Moove()
    {
        float input = _inputActions.Default.Move.ReadValue<float>() * Time.deltaTime * _speed;
        _animator.SetFloat(_mooveValue, input);
        _characterController.Move(new Vector3(input, 0, 0));
    }
}
