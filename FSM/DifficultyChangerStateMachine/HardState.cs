public class HardState : DifficultyState
{
    public HardState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit) : base(key, stateMachine, stateToTransit)
    {
    }
}
