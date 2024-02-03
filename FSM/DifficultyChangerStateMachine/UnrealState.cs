public class UnrealState : DifficultyState
{
    public UnrealState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
    }
}
