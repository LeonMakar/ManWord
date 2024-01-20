using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public EState StateKey { get; private set; }
    public Action<EState> ChangeStateAction;
    protected bool IsTransitionStart = false;

    public BaseState(EState key)
    {
        StateKey = key;
    }
    public abstract void EnterToState();
    public abstract void ExitFromState();
    public abstract void UpdateState();
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnTriggerExit(Collider other) { }
    public virtual void OnTriggerStay(Collider other) { }
}

