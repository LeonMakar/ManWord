using UnityEngine;

public class SuperEasyState : DifficultyState
{
    public SuperEasyState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {

    }
}
