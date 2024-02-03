public class MediumState : DifficultyState
{
    public MediumState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) 
        : base(key, stateMachine, stateToTransit)
    {
    }
}

