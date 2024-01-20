using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;
    private bool _isTransitioningProccesWay = false;
    protected void StartStateMachine(EState startState)
    {
        CurrentState = States[startState];
        CurrentState.ChangeStateAction += TransitionToNextState;
        CurrentState.EnterToState();
    }

    private void Update()
    {
        if (!_isTransitioningProccesWay)
            CurrentState.UpdateState();
    }

    private void TransitionToNextState(EState nextStateKey)
    {
        _isTransitioningProccesWay = true;
        CurrentState.ExitFromState();
        CurrentState.ChangeStateAction -= TransitionToNextState;
        CurrentState = States[nextStateKey];
        CurrentState.ChangeStateAction += TransitionToNextState;
        CurrentState.EnterToState();
        _isTransitioningProccesWay = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    protected void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);

    }

    protected void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);

    }

}

