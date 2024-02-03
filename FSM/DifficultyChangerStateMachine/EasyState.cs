public class EasyState : DifficultyState
{
    public EasyState(DifficultyChangerStateMachine.DificultyStage key, DifficultyChangerStateMachine stateMachine, DifficultyChangerStateMachine.DificultyStage stateToTransit)
        : base(key, stateMachine, stateToTransit)
    {
    }
}
