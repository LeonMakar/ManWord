public class SoftHardState : DifficultyState
{
    public SoftHardState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
    }
}

